using System;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueLine
    {
        public string id;
        public string speaker;
        public string message;
        public string nextID;
    }
}