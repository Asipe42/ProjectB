using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Modin
{
    public class DialogueChapter : SerializedScriptableObject
    {
        public List<DialogueSequence> Sequences;
    }
}