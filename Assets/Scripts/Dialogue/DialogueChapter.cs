using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueChapter", menuName = "Modin/Dialogue/Chapter")]
    public class DialogueChapter : SerializedScriptableObject
    {
        public string id;
        public string nextID;
        public List<DialogueSequence> sequences;

        public DialogueSequence GetFirstSequence() => sequences.Count > 0 ? sequences[0] : null;
        public DialogueSequence GetLastSequence() => sequences.Count > 0 ? sequences[^1] : null;
        
        public bool TryGetSequence(string id, out DialogueSequence result)
        {
            result = sequences.Find(x => x.id == id);
            return result != null;
        }
        
        public bool TryGetNextSequence(DialogueSequence currentSequence, out DialogueSequence nextSequence)
        {
            int index = sequences.FindIndex(x => x.id == currentSequence.nextID);
            if (index >= 0 && index < sequences.Count - 1)
            {
                nextSequence = sequences[index + 1];
                return true;
            }

            nextSequence = null;
            return false;
        }
    }
}