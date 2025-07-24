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
        
        private void ShowCurrentLine()
        {
            // UI 관리 객체
            // UI 관리 객체 없이. System~ 직접 관리한다.
        }
        
        private void NextLine()
        {
            
        }
    }
}