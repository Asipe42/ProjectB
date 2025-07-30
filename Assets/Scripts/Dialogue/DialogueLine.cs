using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    [Serializable]
    public class DialogueLine
    {
        [BoxGroup("필수")] [ReadOnly] 
        public string id;
        
        [BoxGroup("필수")] [ReadOnly] 
        public string speaker;
        
        [BoxGroup("필수")] [ReadOnly, TextArea] 
        public string message;
        
        [BoxGroup("필수")] [FoldoutGroup("필수/분기")] 
        public DialogueBranch[] branches = new DialogueBranch[1];

        [BoxGroup("옵션")] [FoldoutGroup("옵션/비주얼")] 
        [PreviewField] 
        [LabelText("배경")] 
        public Sprite background;
        
        [BoxGroup("옵션")] [FoldoutGroup("옵션/비주얼")] 
        public DialogueVisual[] visuals;

        [BoxGroup("옵션")] [FoldoutGroup("옵션/사운드")] 
        [LabelText("배경음")] 
        public string musicKey;
        
        [BoxGroup("옵션")] [FoldoutGroup("옵션/사운드")] 
        [LabelText("음성")] 
        public string voiceKey;
        
        [BoxGroup("옵션")] [FoldoutGroup("옵션/사운드")]
        [LabelText("효과음")] 
        public string soundKey;
        
        public string GetNextID()
        {
            if (branches != null && branches.Length == 1)
                return branches[0].nextID;

            return null;
        }
    }
}