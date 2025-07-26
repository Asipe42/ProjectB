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
            
            DialogueSystem dialogueSystem = Object.FindFirstObjectByType<DialogueSystem>();
            if (dialogueSystem == null)
            {
                Debug.LogError($"{nameof(DialogueSystem)}이 유효하지 않습니다.");
                return;
            }

            dialogueSystem.Init(Snapshot);
            EditorApplication.isPlaying = true;
        }
    }
}