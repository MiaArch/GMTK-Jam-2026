using System;
using UnityEngine;
using Utils;

namespace UI
{
    public class PauseUI: Singleton<PauseUI>
    {
        [SerializeField] private GameObject pauseUI;
        private bool isPaused;
        
        public void TogglePaused()
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePaused();
            }
        }
    }
}