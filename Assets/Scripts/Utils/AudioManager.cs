using UnityEngine;

namespace Utils
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] AudioSource sfxSource;
        [SerializeField] AudioSource musicSource;
        private float defaultPitch = 1f;

        public void PlaySFX(AudioClip clip)
        {
            sfxSource.pitch = defaultPitch;
            sfxSource.PlayOneShot(clip);
        }

        public void PlaySFXWithPitchShifting(AudioClip clip, float maxPitch, float minPitch)
        {
            sfxSource.pitch = Random.Range(minPitch, maxPitch);
            sfxSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            sfxSource.pitch = defaultPitch;
            if (musicSource.clip == clip)
                return;

            musicSource.clip = clip;
            musicSource.Play();
        }
    }
}