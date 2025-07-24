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
        public List<DialogueLine> Lines;

        public bool TryGetLine(string id, out DialogueLine result)
        {
            result = Lines.Find(x => x.ID == id);
            return result != null;
        }
    }
}