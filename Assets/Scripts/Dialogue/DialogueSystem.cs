using UnityEngine;

namespace Modin
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueDB dialogueDB;
        [SerializeField] private DialogueUI dialogueUI;
        
        private DialogueChapter currentChapter;
        private DialogueSequence currentSequence;
        private DialogueLine currentLine;

        public void Initialize(DialogueSnapshot snapshot)
        {
            if (!dialogueDB.TryResolveSnapshot(snapshot, out currentChapter, out currentSequence, out currentLine))
            {
                Debug.LogError("");
                return;
            }
        }

        public void CleanUp()
        {
            currentChapter = null;
            currentSequence = null;
            currentLine = null;
        }
        
        public void Show()
        {
            dialogueUI.UpdateViews(currentLine);
        }
        
        public void Next()
        {
            
        }
    }
}