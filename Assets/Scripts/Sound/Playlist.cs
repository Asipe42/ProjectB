using UnityEngine;
using System.Collections;

namespace Modin
{
    public class Playlist : MonoBehaviour
    {
        public AudioSource previousChannel;
        public AudioSource activeChannel;

        public void Play(AudioClip clip)
        {
            activeChannel.clip = clip;
            activeChannel.volume = 1 * SoundManager.Instance.MusicVolume;
            activeChannel.Play();
        }

        public void CrossFade(AudioClip clip, float fadeTime)
        {
            StopAllCoroutines();
            
            AudioSource temp = previousChannel;
            previousChannel = activeChannel;
            activeChannel = temp;

            activeChannel.clip = clip;
            activeChannel.volume = 0f;
            activeChannel.Play();

            StartCoroutine(CrossFadeRoutine(fadeTime));
        }

        private IEnumerator CrossFadeRoutine(float fadeTime)
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float progress = t / fadeTime;
                previousChannel.volume = (1f - progress) * SoundManager.Instance.MusicVolume;
                activeChannel.volume = progress;
                yield return null;
            }

            previousChannel.Stop();
            activeChannel.volume = 1f * SoundManager.Instance.MusicVolume;
        }

        public void FadeIn(float fadeTime)
        {
            StopAllCoroutines();
            StartCoroutine(FadeInRoutine(fadeTime));
        }

        public void FadeOut(float fadeTime)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutRoutine(fadeTime));
        }

        private IEnumerator FadeInRoutine(float fadeTime)
        {
            activeChannel.volume = 0f;
            float time = 0f;
            while (time < fadeTime)
            {
                time += Time.deltaTime;
                activeChannel.volume = (time / fadeTime) * SoundManager.Instance.MusicVolume;
                yield return null;
            }
            activeChannel.volume = 1f * SoundManager.Instance.MusicVolume;
        }

        private IEnumerator FadeOutRoutine(float fadeTime)
        {
            float time = 0f;
            float startVolume = activeChannel.volume;
            while (time < fadeTime)
            {
                time += Time.deltaTime;
                activeChannel.volume = startVolume * (1f - (time / fadeTime));
                yield return null;
            }
            activeChannel.volume = 0f;
            activeChannel.Stop();
        }
    }
}
