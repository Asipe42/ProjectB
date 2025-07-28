using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueLine
    {
        [FoldoutGroup("Base")] public string id;
        [FoldoutGroup("Base")] public string speaker;
        [FoldoutGroup("Base")] [TextArea] public string message;
        [FoldoutGroup("Base")] public string nextID;

        [FoldoutGroup("Visual")] public Sprite background;
        [FoldoutGroup("Visual")] public DialogueVisual[] visuals;

        [FoldoutGroup("Sound")] public string musicKey;
        [FoldoutGroup("Sound")] public string voiceKey;
        [FoldoutGroup("Sound")] public string soundKey;
    }
}