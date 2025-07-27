using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Modin/Core/GameConfig")]
    public class GameConfig : SerializedScriptableObject
    {
        public float loadingDuration;
        public float introDuration;
    }
}