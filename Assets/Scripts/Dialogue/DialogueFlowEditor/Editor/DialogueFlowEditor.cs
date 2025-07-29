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
            /*
             * 동작
             *  1. GraphView 생성
             *  2. Toolbar 생성
             */

            InitializeGraphView();
            InitializeToolbar();
        }
        
        private void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }

        private void InitializeToolbar()
        {
            Toolbar toolbar = new Toolbar();

            Button selectButton = new Button(() => { SelectSequence(); })
            {
                text = "New Select"
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
        
        private void InitializeGraphView()
        {
            graphView = new DialogueGraphView
            {
                name = "Dialogue Graph"
            };
            
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void SelectSequence()
        {
            string path = EditorUtility.OpenFilePanel("Select Dialogue Sequence", "Assets/Data/Dialogue", string.Empty);
            
            if (string.IsNullOrEmpty(path))
                return;
            
            string assetPath = "Assets" + path.Substring(Application.dataPath.Length);
            DialogueSequence sequence = AssetDatabase.LoadAssetAtPath<DialogueSequence>(assetPath);
            if (sequence == null)
            {
                Debug.LogError("선택한 파일이 유효하지 않습니다.");
                return;
            }
            
            graphView.SelectSequence(sequence);
        }
        
        private void SaveGraph()
        {
            DialogueSequence selectedSequence = graphView.SelectedSequence;
            if (selectedSequence == null)
            {
                EditorUtility.DisplayDialog("안내", "저장할 그래프가 선택되지 않았습니다.", "확인");
                return;
            }
            
            DialogueGraphSO graphSO = ScriptableObject.CreateInstance<DialogueGraphSO>();
            graphSO.sequence = selectedSequence;
            graphSO.nodePositions = graphView.CollectNodePositions(); 
            
            string savePath = $"Assets/Data/Dialogue/Graph/{selectedSequence.id}.asset";
            AssetDatabase.CreateAsset(graphSO, savePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            EditorUtility.DisplayDialog("안내", $"성공적으로 그래프를 저장하였습니다.\n경로: {savePath}", "확인");
        }
    
        private void LoadGraph()
        {
            string path = EditorUtility.OpenFilePanel("Select Dialogue Sequence", "Assets/Data/Dialogue", string.Empty);
            
            if (string.IsNullOrEmpty(path))
                return;
            
            string assetPath = "Assets" + path.Substring(Application.dataPath.Length);
            DialogueGraphSO graphSO = AssetDatabase.LoadAssetAtPath<DialogueGraphSO>(assetPath);
            if (graphSO == null)
            {
                Debug.LogError("선택한 파일이 유효하지 않습니다.");
                return;
            }
            
            graphView.LoadSequence(graphSO);
        }
    }
}