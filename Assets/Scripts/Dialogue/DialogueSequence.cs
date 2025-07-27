using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueSequence", menuName = "Modin/Dialogue/Sequence")]
    public class DialogueSequence : SerializedScriptableObject
    {
        public string id;
        public string nextID;
        public List<DialogueLine> lines;

        public DialogueLine GetFirstLine() => lines.Count > 0 ? lines[0] : null;
        public DialogueLine GetLastLine() => lines.Count > 0 ? lines[^1] : null;
        
        public bool TryGetLine(string id, out DialogueLine result)
        {
            result = lines.Find(x => x.id == id);
            return result != null;
        }

        public bool TryGetNextLine(DialogueLine currentLine, out DialogueLine nextLine)
        {
            int index = lines.FindIndex(x => x.id == currentLine.nextID);
            if (index >= 0 && index < lines.Count - 1)
            {
                nextLine = lines[index + 1];
                return true;
            }

            nextLine = null;
            return false;
        }
    }
}