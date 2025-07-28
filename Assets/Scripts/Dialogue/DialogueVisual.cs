using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueVisual
    {
        public Sprite sprite;
        public int order;
        public VisualSlotType slotType;
        public VisualAnimationType appearAnimationType;
        public VisualAnimationType disappearAnimationType;
    }
}