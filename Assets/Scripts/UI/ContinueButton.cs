using UnityEngine;

namespace UI
{
    public class ContinueButton : MonoBehaviour
    {
        public void Continue()
        {
            PauseManager.Instance.SetPaused(false);
        }
    }
}