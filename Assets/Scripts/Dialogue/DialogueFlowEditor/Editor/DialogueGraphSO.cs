using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    public class DialogueGraphSO : SerializedScriptableObject
    {
        public DialogueSequence sequence;
        public Dictionary<string, Vector2> nodePositions;
    }
}