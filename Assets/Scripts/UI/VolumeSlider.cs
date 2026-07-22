using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] AudioMixer mixer;

        public void SetVolume(float value)
        {
            mixer.SetFloat("MasterVolume",
                Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
        }
    }
}