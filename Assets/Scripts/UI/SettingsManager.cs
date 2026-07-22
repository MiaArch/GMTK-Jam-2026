using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace UI
{
    public class SettingsManager : PersistentSingleton<SettingsManager>
    {
        [SerializeField] AudioMixer mixer;

        const string MasterVolumeKey = "MasterVolume";
        const string FullscreenKey = "Fullscreen";

        public float MasterVolume { get; private set; } = 1f;
        public bool Fullscreen { get; private set; } = true;

        protected override void Awake()
        {
            base.Awake();
            Load();
        }

        public void SetMasterVolume(float volume)
        {
            MasterVolume = Mathf.Clamp(volume, 0.0001f, 1f);

            mixer.SetFloat(
                "MasterVolume",
                Mathf.Log10(MasterVolume) * 20f);

            PlayerPrefs.SetFloat(MasterVolumeKey, MasterVolume);
            PlayerPrefs.Save();
        }

        public void SetFullscreen(bool fullscreen)
        {
            Fullscreen = fullscreen;

            Screen.fullScreen = fullscreen;

            PlayerPrefs.SetInt(FullscreenKey, fullscreen ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            MasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
            Fullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;

            mixer.SetFloat(
                "MasterVolume",
                Mathf.Log10(MasterVolume) * 20f);

            Screen.fullScreen = Fullscreen;
        }
    }
}