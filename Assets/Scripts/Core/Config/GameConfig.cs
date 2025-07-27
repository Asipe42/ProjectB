using Sirenix.OdinInspector;
using UnityEngine;

namespace Modin
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Modin/Core/GameConfig")]
    public class GameConfig : SerializedScriptableObject
    {
        public IntroConfig IntroConfig;
        public TitleConfig TitleConfig;
        public GameplayConfig GameplayConfig;
        public LoadingConfig LoadingConfig;
    }
}