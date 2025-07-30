using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modin
{
    public class DialogueNode : Node
    {
        public DialogueLine Line { get; private set; }

        private int branchCount;
        
        public DialogueNode(DialogueLine line)
        {
            this.Line = line;
            base.title = line.id;

            /*
             * 과정
             *  1. speaker, message를 포함한 텍스트 필드 생성
             *  2. 입력 포트 생성
             *  3. 출력 포트 생성
             *  4. 분기 추가 버튼 생성
             */

            InitializeTextField();
            InitializeInputPort();
            InitializeOutputPort();
            InitializeButtons();
            
            RefreshPorts();
            RefreshExpandedState();
        }

        private void InitializeTextField()
        {
            string text = string.IsNullOrEmpty(Line.speaker) ? Line.message : $"{Line.speaker}: {Line.message}";
            TextField textField = new TextField(text)
            {
                multiline = true,
                isReadOnly = true,
                style =
                {
                    whiteSpace = WhiteSpace.Normal,
                    maxWidth = 200,
                    paddingLeft = 5,
                    paddingRight = 5,
                    paddingTop = 5,
                    paddingBottom = 5,
                    unityTextAlign = TextAnchor.MiddleLeft,
                }
            };
            mainContainer.Add(textField);
        }
        
        private void InitializeInputPort()
        {
            AddInputPort();
        }
        
        private void InitializeOutputPort()
        {
            foreach (var each in Line.branches)
                AddOutputPort();
        }
        
        private void InitializeButtons()
        {
            titleContainer.style.height = 60;
            
            VisualElement buttonContainer = new VisualElement();
            buttonContainer.style.flexDirection = FlexDirection.Column;
            buttonContainer.style.marginTop = 5;
            buttonContainer.style.marginRight = 5;
            
            Button addBranchButton = new Button(OnClickAddBranch)
            {
                text = "Add Branch"
            };
            buttonContainer.Add(addBranchButton);
            
            Button removeBranchButton = new Button(() => OnClickRemoveBranch(null))
            {
                text = "Remove Branch"
            };
            buttonContainer.Add(removeBranchButton);
            
            titleButtonContainer.Add(buttonContainer);
        }

        private void AddInputPort()
        {
            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(DialogueLine));
            inputPort.portName = "Previous";
            inputContainer.Add(inputPort);
        }
        
        private void AddOutputPort()
        {
            int index = branchCount;
            branchCount++;
            
            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            container.style.alignItems = Align.Center;
            
            Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(DialogueLine));
            outputPort.portName = $"Branch {branchCount - 1}";
            
            TextField choiceField = new TextField()
            {
                value = Line.branches[index].choiceText,
            };
            
            choiceField.RegisterValueChangedCallback
            (
                @event =>
                {
                    Line.branches[index].choiceText = @event.newValue;
                }
            );
            
            container.Add(choiceField);
            container.Add(outputPort);
            
            outputContainer.Add(container);
        }
        
        private void OnClickAddBranch()
        {
            DialogueBranch[] oldBranches = Line.branches;
            DialogueBranch[] newBranches = new DialogueBranch[branchCount];
            oldBranches.CopyTo(newBranches, 0);
            
            newBranches[branchCount - 1] = new DialogueBranch();
            Line.branches = newBranches;
            
            AddOutputPort();
            
            RefreshPorts();
            RefreshExpandedState();
        }

        private void OnClickRemoveBranch(Port port)
        {
            if (branchCount <= 1)
                return;

            branchCount--;
            
            DialogueBranch[] oldBranches = Line.branches;
            if (oldBranches == null || oldBranches.Length == 0)
            {
                Debug.LogError($"{nameof(DialogueBranch)}가 유효하지 않습니다.");
                return;
            }
            
            DialogueBranch[] newBranches = new DialogueBranch[branchCount];
            Array.Copy(oldBranches, 0, newBranches, 0, branchCount);
            Line.branches = newBranches;
            
            if (outputContainer.childCount > 0)
            {
                int reoveIndex = outputContainer.childCount - 1;
                Port portToRemove = outputContainer[reoveIndex] as Port;
                
                if (portToRemove == null)
                {
                    Debug.LogError($"{reoveIndex}번 {nameof(Port)}가 유효하지 않습니다.");
                    return;
                }

                List<Edge> edges = portToRemove.connections.ToList();
                foreach (var each in edges)
                {
                    each.input?.Disconnect(each);
                    each.output?.Disconnect(each);
                    each.parent?.Remove(each);
                }
                
                outputContainer.Remove(portToRemove);
            }

            RefreshPorts();
            RefreshExpandedState();
        }
    }
}