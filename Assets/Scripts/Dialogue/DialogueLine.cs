using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueLine
    {
        public string id;
        public string speaker;
        [TextArea] public string message;
        public string nextID;

        public Sprite background;
        public DialogueVisual[] visuals;
    }
}