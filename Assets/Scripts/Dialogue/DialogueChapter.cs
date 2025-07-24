using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueChapter", menuName = "Modin/Dialogue/Chapter")]
    public class DialogueChapter : SerializedScriptableObject
    {
        public List<DialogueSequence> Sequences;
    }
}