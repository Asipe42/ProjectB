using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modin
{
    public class DialogueGraphView : GraphView
    {
        public DialogueGraphView()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
            
            CreateNode(new DialogueLine());
        }

        public void CreateNode(DialogueLine line)
        {
            DialogueNode node = new DialogueNode(line);
            node.SetPosition(new Rect());
            AddElement(node);
        }
    }
}