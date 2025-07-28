using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Modin
{
    public class DialoguePlayEditor : OdinEditorWindow
    {
        public DialogueSnapshot Snapshot;
        
        [MenuItem("Tools/Dialogue/Play Editor")]
        private static void OpenWindow()
        {
            var window = GetWindow<DialoguePlayEditor>("Dialogue Play Editor");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }
        
        [Button]
        private void Play()
        {
            if (EditorApplication.isPlaying)
            {
                Debug.LogWarning("이미 플레이 모드입니다.");
                return;
            }
            
            DialogueManager dialogueManager = Object.FindFirstObjectByType<DialogueManager>();
            if (dialogueManager == null)
            {
                Debug.LogError($"{nameof(DialogueManager)}이 유효하지 않습니다.");
                return;
            }

            dialogueManager.Init(Snapshot);
            EditorApplication.isPlaying = true;
        }
    }
}