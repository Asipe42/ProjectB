using UnityEditor.Experimental.GraphView;

namespace Modin
{
    public class DialogueNode : Node
    {
        public Port InputPort;
        public Port OutputPort;
        private DialogueLine line;
        
        public DialogueNode(DialogueLine line)
        {
            this.line = line;
            title = $"{line.speaker}: {line.id}";

            InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(bool));
            InputPort.portName = "Input";
            inputContainer.Add(InputPort);

            OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            OutputPort.portName = "Output";
            outputContainer.Add(OutputPort);

            RefreshExpandedState();
            RefreshPorts();
        }
    }
}