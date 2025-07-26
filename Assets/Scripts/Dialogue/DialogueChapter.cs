using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueChapter", menuName = "Modin/Dialogue/Chapter")]
    public class DialogueChapter : SerializedScriptableObject
    {
        public string ID;
        public string NextID;
        public List<DialogueSequence> Sequences;

        public DialogueSequence GetFirstSequence() => Sequences.Count > 0 ? Sequences[0] : null;
        public DialogueSequence GetLastSequence() => Sequences.Count > 0 ? Sequences[^1] : null;
        
        public bool TryGetSequence(string id, out DialogueSequence result)
        {
            result = Sequences.Find(x => x.ID == id);
            return result != null;
        }
        
        public bool TryGetNextSequence(DialogueSequence currentSequence, out DialogueSequence nextSequence)
        {
            int index = Sequences.FindIndex(x => x.ID == currentSequence.NextID);
            if (index >= 0 && index < Sequences.Count - 1)
            {
                nextSequence = Sequences[index + 1];
                return true;
            }

            nextSequence = null;
            return false;
        }
    }
}