using UnityEngine;

namespace Modin
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        /*
         * 역할
         *  - BGM 재생
         *  - 효과음 재생
         *  - 볼륨 제어
         *  - 페이드/인 아웃 등 연출 효과
         */

        [SerializeField] private SoundConfig soundConfig;
        
        protected override void Awake()
        {
            base.Awake();
            
            
        }

        public void PlayBGM(string key)
        {
            
        }

        public void PlaySFX(string key)
        {
            
        }

        public void SetMasterVolume()
        {
            
        }

        public void SetBGMVolume()
        {
            
        }

        public void SetSFXVolume()
        {
            
        }
    }
}