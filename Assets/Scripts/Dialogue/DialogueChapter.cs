using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueChapter", menuName = "Modin/Dialogue/Chapter")]
    public class DialogueChapter : SerializedScriptableObject
    {
        public string ID;
        public List<DialogueSequence> Sequences;

        public bool TryGetSequence(string id, out DialogueSequence result)
        {
            result = Sequences.Find(x => x.ID == id);
            return result != null;
        }
    }
}