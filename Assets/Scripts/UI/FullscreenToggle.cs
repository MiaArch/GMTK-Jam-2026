using UnityEngine;

namespace UI
{
    public class FullscreenToggle : MonoBehaviour
    {
        public void SetFullscreen(bool fullscreen)
        {
            Screen.fullScreen = fullscreen;
        }
    }
}