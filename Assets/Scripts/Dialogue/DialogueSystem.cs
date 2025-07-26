using System;
using System.Linq;
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
        
        public void Init(DialogueSnapshot snapshot)
        {
            if (!dialogueDB.TryResolveSnapshot(snapshot, out currentChapter, out currentSequence, out currentLine))
            {
                Debug.LogError($"{nameof(DialogueSnapshot)}이 유효하지 않습니다.");
                return;
            }
            
            dialogueUI.Initialize();
            Show();
        }
        
        public void CleanUp()
        {
            currentChapter = null;
            currentSequence = null;
            currentLine = null;
            
            dialogueUI.CleanUp();
        }
        
        public void Show()
        {
            dialogueUI.UpdateViews(currentLine);
        }
        
        public bool TryAdvanceDialogue()
        {
            if (currentSequence.TryGetNextLine(currentLine, out currentLine))
            {
                Show();
                return true;
            }

            if (currentChapter.TryGetNextSequence(currentSequence, out currentSequence))
            {
                currentLine = currentSequence.GetFirstLine();
                Show();
                return true;
            }

            if (dialogueDB.TryGetNextChapter(currentChapter, out currentChapter))
            {
                currentSequence = currentChapter.GetFirstSequence();
                currentLine = currentSequence.GetFirstLine();
                Show();
                return true;
            }
            
            return false;
        }
    }
}