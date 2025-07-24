using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    [CreateAssetMenu(fileName = "DialogueDB", menuName = "Modin/Dialogue/DB")]
    public class DialogueDB : SerializedScriptableObject
    {
        public List<DialogueChapter> Chapters;
    }
}