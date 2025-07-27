using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    public class DialogueManager : MonoSingleton<DialogueManager>, IInputHandler
    {
        [SerializeField] private DialogueSnapshot sampleSnapshot;
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
            
            dialogueUI.Open(new DialogueUIModel()
            {
                OnNext = Next
            });
            Show();
            
            InputManager.Instance.RegisterHandler(this);
        }
        
        public void CleanUp()
        {
            currentChapter = null;
            currentSequence = null;
            currentLine = null;
            
            dialogueUI.Close();
        }
        
        public void Show()
        {
            dialogueUI.UpdateViews(currentLine);
        }
        
        public void Next()
        {
            if (currentSequence.TryGetNextLine(currentLine, out currentLine))
                Show();

            if (currentChapter.TryGetNextSequence(currentSequence, out currentSequence))
            {
                currentLine = currentSequence.GetFirstLine();
                Show();
            }

            if (dialogueDB.TryGetNextChapter(currentChapter, out currentChapter))
            {
                currentSequence = currentChapter.GetFirstSequence();
                currentLine = currentSequence.GetFirstLine();
                Show();
            }
        }

        public void OnSpacebar()
        {
            Next();
        }
    }
}