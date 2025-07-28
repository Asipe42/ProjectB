using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Modin
{
    public class DialoguePlayTool : OdinEditorWindow
    {
        public DialogueSnapshot Snapshot;
        
        [MenuItem("Tools/Dialogue/PlayTool")]
        private static void OpenWindow()
        {
            GetWindow<DialoguePlayTool>("Dialogue Play Tool").Show();
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