using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modin
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        /*
         * 역할
         *  - 사운드(배경음, 효과음) 재생
         *  - 볼륨 제어
         *  - 페이드/인 아웃 등 연출 효과
         */

        [SerializeField] private Playlist playlist; 
        [SerializeField] private List<AudioSource> channels;
        [SerializeField] private SoundConfig soundConfig;

        public float MusicVolume { get; set; } // 배경음
        public float VoiceVolume { get; set; } // 목소리 (더빙 등)
        public float SoundVolume { get; set; } // 효과음

        public void PlayMusic(string key)
        {
            if (!soundConfig.clipMap.TryGetValue(key, out var clip) || clip == null)
            {
                Debug.LogWarning($"SoundManager: 사운드 키 '{key}'를 찾을 수 없습니다.");
                return;
            }

            playlist.Play(clip);
        }

        public void PlayVoice(string key)
        {
            if (!soundConfig.clipMap.TryGetValue(key, out var clip) || clip == null)
            {
                Debug.LogWarning($"SoundManager: 사운드 키 '{key}'를 찾을 수 없습니다.");
                return;
            }

            if (!TryGetFreeChannel(out var channel))
            {
                Debug.LogWarning("SoundManager: 사용 가능한 오디오 채널이 없습니다.");
                return;
            }
            
            channel.clip = clip;
            channel.volume = VoiceVolume;
            channel.Play();
        }
        
        public void PlaySound(string key)
        {
            if (!soundConfig.clipMap.TryGetValue(key, out var clip) || clip == null)
            {
                Debug.LogWarning($"SoundManager: 사운드 키 '{key}'를 찾을 수 없습니다.");
                return;
            }

            if (!TryGetFreeChannel(out var channel))
            {
                Debug.LogWarning("SoundManager: 사용 가능한 오디오 채널이 없습니다.");
                return;
            }
            
            channel.clip = clip;
            channel.volume = SoundVolume;
            channel.Play();
        }
        
        private bool TryGetFreeChannel(out AudioSource result)
        {
            foreach (var channel in channels)
            {
                if (!channel.isPlaying)
                {
                    result = channel;
                    return true;
                }
            }
            
            result = null;
            return false;
        }
    }
}