using Resource;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] AudioSource sfxSource;
        [SerializeField] AudioSource musicSource;

        [SerializeField] private AudioClip UIButtonClick;
        [Header("Distortion")]
        [SerializeField] private int cluesForMaxEffect = 5;
        [SerializeField] private float minPitch = 0.8f;

        private float defaultPitch = 1f;

        float CurrentPitch
        {
            get
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    return 1f;
                }
                int clues = ResourceManager.Instance.GetAmount(ResourceType.Clues);
                float t = Mathf.Clamp01((float)clues / cluesForMaxEffect);
                return Mathf.Lerp(defaultPitch, minPitch, t);
            }
        }

        public void PlayButtonClick()
        {
            float basePitch = CurrentPitch;
            sfxSource.pitch = basePitch * Random.Range(0.95f, 1.05f);
            sfxSource.PlayOneShot(UIButtonClick);
        }

        public void PlaySFX(AudioClip clip)
        {
            sfxSource.pitch = CurrentPitch;
            sfxSource.PlayOneShot(clip);
        }

        public void PlaySFXWithPitchShifting(AudioClip clip, float maxPitch, float minimPitch)
        {
            float basePitch = CurrentPitch;
            sfxSource.pitch = basePitch * Random.Range(minimPitch, maxPitch);
            sfxSource.PlayOneShot(clip);
        }

        public void LoopSFX()
        {
            sfxSource.loop = true;
        }

        public void StopSFX()
        {
            sfxSource.Stop();
            sfxSource.loop = false;
        }

        public void PlayMusic(AudioClip clip)
        {
            sfxSource.pitch = defaultPitch;
            if (musicSource.clip == clip)
                return;

            musicSource.clip = clip;
            musicSource.Play();
        }
        
        private void Update()
        {
            musicSource.pitch = CurrentPitch;
        }
    }
}