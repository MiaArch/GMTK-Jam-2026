using UnityEngine;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] GameObject panel;

        public void Open()
        {
            panel.SetActive(true);
        }

        public void Close()
        {
            panel.SetActive(false);
        }
    }
}