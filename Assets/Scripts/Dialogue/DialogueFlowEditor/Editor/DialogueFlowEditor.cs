using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modin
{
    public class DialogueFlowEditor : EditorWindow
    {
        private DialogueGraphView graphView;
    
        [MenuItem("Tools/Dialogue/Flow Editor")]
        public static void OpenWindow()
        {
            var window = GetWindow<DialogueFlowEditor>("Dialogue Flow Editor");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }
        
        private void OnEnable()
        {
            graphView = new DialogueGraphView
            {
                name = "Dialogue Graph"
            };
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
            
            Toolbar toolbar = new Toolbar();

            Button selectButton = new Button(() => { SelectSequence(); })
            {
                text = "Select"
            };
            toolbar.Add(selectButton);
            
            Button saveButton = new Button(() => { SaveGraph(); })
            {
                text = "Save"
            };
            toolbar.Add(saveButton);
            
            Button loadButton = new Button(() => { LoadGraph(); })
            {
                text = "Load"
            };
            toolbar.Add(loadButton);
            
            rootVisualElement.Add(toolbar);
        }
        
        private void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }

        private void SelectSequence()
        {
            Debug.Log($"{nameof(SelectSequence)}");
        }
        
        private void SaveGraph()
        {
            Debug.Log($"{nameof(SaveGraph)}");
        }
    
        private void LoadGraph()
        {
            Debug.Log($"{nameof(LoadGraph)}");
        }
    }
}