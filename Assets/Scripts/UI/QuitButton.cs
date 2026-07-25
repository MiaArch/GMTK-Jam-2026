using UnityEngine;
using Utils;

namespace UI
{
    public class QuitButton : MonoBehaviour
    {
        public void Quit()
        {
            AudioManager.Instance.PlayButtonClick();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
            
        }
    }
}