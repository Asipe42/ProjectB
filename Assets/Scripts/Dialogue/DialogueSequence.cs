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
    }
}