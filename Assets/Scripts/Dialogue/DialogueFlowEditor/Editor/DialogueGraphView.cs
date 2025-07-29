using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modin
{
    public class DialogueGraphView : GraphView
    {
        public DialogueSequence SelectedSequence { get; private set; }
        
        public DialogueGraphView()
        {
            InitializeZoom();
            InitializeManipulator();
            InitializeGridBackground();
            
            this.graphViewChanged = OnGraphViewChanged;
        }

        public void SelectSequence(DialogueSequence sequence)
        {
            if (sequence == null)
            {
                Debug.LogError($"{nameof(sequence)}가 유효하지 않습니다.");
                return;
            }

            SelectedSequence = sequence;

            Dictionary<string, Vector2> defaultPositions = new Dictionary<string, Vector2>();
            for (int i = 0; i < sequence.lines.Count; i++)
            {
                defaultPositions[sequence.lines[i].id] = new Vector2(300 * i, 0);
            }

            BuildGraph(sequence, defaultPositions);
        }

        public void LoadSequence(DialogueGraphSO graphSO)
        {
            if (graphSO.sequence == null)
            {
                Debug.LogError($"{nameof(graphSO.sequence)}가 유효하지 않습니다.");
                return;
            }

            SelectedSequence = graphSO.sequence;
            
            BuildGraph(SelectedSequence,  graphSO.nodePositions);
        }
        
        public Dictionary<string, Vector2> CollectNodePositions()
        {
            Dictionary<string, Vector2> positions = new Dictionary<string, Vector2>();
            foreach (DialogueNode node in nodes)
            {
                positions[node.Line.id] = node.GetPosition().position; 
            }
    
            return positions;
        }
        
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            /*
             * 포트 유효성
             *  - Cond1: 같은 포트가 아니어야 한다.
             *  - Cond2: 같은 방향이 아니어야 한다.
             *  - Cond3: 포트 타입이 같아야 한다.
             */
            
            List<Port> compatiblePorts = new List<Port>();
            foreach (var port in ports)
            {
                if (port == startPort)
                    continue;

                if (port.direction == startPort.direction)
                    continue;

                if (port.portType != startPort.portType)
                    continue;

                compatiblePorts.Add(port);
            }

            return compatiblePorts;
        }
        
        private void InitializeZoom()
        {
            this.SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        }
        
        private void InitializeManipulator()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private void InitializeGridBackground()
        {
            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }
        
        private void BuildGraph(DialogueSequence sequence, Dictionary<string, Vector2> nodePositions)
        {
            ClearGraph();
            
            Dictionary<string, DialogueNode> nodeMap = new Dictionary<string, DialogueNode>();
            for (int i = 0; i < sequence.lines.Count; i++)
            {
                DialogueLine line = sequence.lines[i];
                
                if (!nodePositions.TryGetValue(line.id, out var  pos))
                {
                    Debug.LogError($"{line.id}가 유효하지 않습니다.");
                    continue;
                }
                
                DialogueNode node = CreateNode(line, pos);
                nodeMap[line.id] = node;
            }

            foreach (var line in sequence.lines)
            {
                if (!nodeMap.TryGetValue(line.id, out DialogueNode currentNode))
                    continue;

                if (line.branches == null)
                    continue;

                for (int branchIndex = 0; branchIndex < line.branches.Length; branchIndex++)
                {
                    DialogueBranch branch = line.branches[branchIndex];
                    
                    if (string.IsNullOrEmpty(branch.nextID))
                        continue;

                    if (!nodeMap.TryGetValue(branch.nextID, out DialogueNode nextNode))
                        continue;

                    Port outputPort = (currentNode.outputContainer[branchIndex] as VisualElement)?
                        .Children()
                        .OfType<Port>()
                        .FirstOrDefault();
                    Port inputPort = nextNode.inputContainer[0] as Port;

                    if (outputPort == null || inputPort == null)
                        continue;

                    Edge edge = new Edge
                    {
                        output = outputPort,
                        input = inputPort
                    };
                    edge.output.Connect(edge);
                    edge.input.Connect(edge);

                    AddElement(edge);
                }
            }
        }
        
        private void ClearGraph()
        {
            foreach (var edge in edges.ToList())
            {
                RemoveElement(edge);
            }
    
            foreach (var node in nodes.ToList())
            {
                RemoveElement(node);
            }
        }
        
        private DialogueNode CreateNode(DialogueLine line, Vector2 position)
        {
            DialogueNode node = new DialogueNode(line);
            node.SetPosition(new Rect(position.x, position.y, 0, 0));
            AddElement(node);

            return node;
        }
        
        private GraphViewChange OnGraphViewChanged(GraphViewChange changes)
        {
            if (changes.edgesToCreate != null)
            {
                foreach (var edge in changes.edgesToCreate)
                {
                    if (edge.input == null || edge.output == null)
                        continue;

                    DialogueNode outputNode = edge.output.node as DialogueNode;
                    DialogueNode inputNode = edge.input.node as DialogueNode;

                    if (outputNode == null || inputNode == null)
                        continue;

                    DialogueLine outputLine = outputNode.Line;
                    DialogueLine inputLine = inputNode.Line;

                    if (outputLine.branches == null || outputLine.branches.Length == 0)
                        continue;
                    
                    string portName = edge.output.portName;
                    if (!portName.StartsWith("Branch "))
                    {
                        Debug.LogError($"{portName}이 유효하지 않습니다.");
                        continue;
                    }            
                    
                    string numberPart = portName.Substring("Branch ".Length);
                    if (!int.TryParse(numberPart, out int result))
                    {
                        Debug.LogError($"{portName} 이름이 유효하지 않습니다.");
                        continue;
                    }
                    
                    int index = result;
                    outputLine.branches[index].nextID = inputLine.id;
                }
            }
            
            return changes;
        }
    }
}