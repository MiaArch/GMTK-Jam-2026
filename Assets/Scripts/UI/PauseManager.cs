using Utils;

namespace UI
{
    using UnityEngine;

    public class PauseManager : PersistentSingleton<PauseManager>
    {
        public bool IsPaused { get; private set; }

        public delegate void PauseStateChanged(bool paused);
        public event PauseStateChanged OnPauseChanged;

        public void Toggle()
        {
            SetPaused(!IsPaused);
        }

        public void SetPaused(bool paused)
        {
            if (IsPaused == paused)
                return;

            IsPaused = paused;
            Time.timeScale = paused ? 0f : 1f;

            OnPauseChanged?.Invoke(paused);
        }

        private void OnApplicationQuit()
        {
            Time.timeScale = 1f;
        }
    }
}