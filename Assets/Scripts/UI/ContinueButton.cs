using UnityEngine;
using Utils;

namespace UI
{
    public class ContinueButton : MonoBehaviour
    {
        public void Continue()
        {
            AudioManager.Instance.PlayButtonClick();
            PauseUI.Instance.TogglePaused();
        }
    }
}