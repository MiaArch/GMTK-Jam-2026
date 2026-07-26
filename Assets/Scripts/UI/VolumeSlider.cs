using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audioSource;
        [SerializeField] private Slider _slider;

        public void Start()
        {
            SetVolume();
        }

        public void SetVolume()
        {
            foreach (var audioS in audioSource)
            {
                audioS.volume = _slider.value;
            }
        }
    }
}