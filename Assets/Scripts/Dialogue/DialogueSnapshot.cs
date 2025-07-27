using System;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueSnapshot
    {
        public string chapterID;
        public string sequenceID;
        public string lineID;
    }
}