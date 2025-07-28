using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueLine
    {
        [FoldoutGroup("필수")] public string id;
        [FoldoutGroup("필수")] public string speaker;
        [FoldoutGroup("필수")] [TextArea] public string message;
        [FoldoutGroup("필수")] public string nextID;

        [FoldoutGroup("옵션")] [BoxGroup("옵션/비주얼")] [PreviewField] public Sprite background;
        [FoldoutGroup("옵션")] [BoxGroup("옵션/비주얼")] public DialogueVisual[] visuals;

        [FoldoutGroup("옵션")] [BoxGroup("옵션/사운드")] public string musicKey;
        [FoldoutGroup("옵션")] [BoxGroup("옵션/사운드")] public string voiceKey;
        [FoldoutGroup("옵션")] [BoxGroup("옵션/사운드")] public string soundKey;
    }
}