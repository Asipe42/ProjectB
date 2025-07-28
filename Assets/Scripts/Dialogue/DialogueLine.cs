using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueLine
    {
        [FoldoutGroup("필수")] [ReadOnly] public string id;
        [FoldoutGroup("필수")] [ReadOnly] public string speaker;
        [FoldoutGroup("필수")] [ReadOnly] [TextArea] public string message;
        [FoldoutGroup("필수")] public string nextID;

        [FoldoutGroup("옵션")] [BoxGroup("옵션/비주얼")] [PreviewField] [LabelText("배경")] public Sprite background;
        [FoldoutGroup("옵션")] [BoxGroup("옵션/비주얼")] public DialogueVisual[] visuals;

        [FoldoutGroup("옵션")] [BoxGroup("옵션/사운드")] [LabelText("배경음")] public string musicKey;
        [FoldoutGroup("옵션")] [BoxGroup("옵션/사운드")] [LabelText("음성")] public string voiceKey;
        [FoldoutGroup("옵션")] [BoxGroup("옵션/사운드")] [LabelText("효과음")] public string soundKey;
    }
}