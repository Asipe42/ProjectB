using System;
using UnityEngine.Serialization;

namespace Modin
{
    [Serializable]
    public class DialogueLine
    {
        public string ID;
        public string Speaker;
        public string Message;
        public string NextID;
    }
}