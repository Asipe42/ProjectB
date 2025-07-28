using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueVisual
    {
        [PreviewField] public Sprite sprite;
        [Range(0, 2)] public int order;
        [EnumToggleButtons] [LabelText("슬롯")] public VisualSlotType slotType;
        [EnumToggleButtons] [LabelText("등장 연출")] public VisualAnimationType appearAnimationType;
        [EnumToggleButtons] [LabelText("퇴장 연출")] public VisualAnimationType disappearAnimationType;
    }
}