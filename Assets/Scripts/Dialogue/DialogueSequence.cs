using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueSequence", menuName = "Modin/Dialogue/Sequence")]
    public class DialogueSequence : SerializedScriptableObject
    {
        public string ID;
        public string NextID;
        public List<DialogueLine> Lines;

        public DialogueLine GetFirstLine() => Lines.Count > 0 ? Lines[0] : null;
        public DialogueLine GetLastLine() => Lines.Count > 0 ? Lines[^1] : null;
        
        public bool TryGetLine(string id, out DialogueLine result)
        {
            result = Lines.Find(x => x.ID == id);
            return result != null;
        }

        public bool TryGetNextLine(DialogueLine currentLine, out DialogueLine nextLine)
        {
            int index = Lines.FindIndex(x => x.ID == currentLine.NextID);
            if (index >= 0 && index < Lines.Count - 1)
            {
                nextLine = Lines[index + 1];
                return true;
            }

            nextLine = null;
            return false;
        }
    }
}