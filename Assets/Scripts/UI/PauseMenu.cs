using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject panel;

        void Awake()
        {
            PauseManager.Instance.OnPauseChanged += ToggleUI;
            panel.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseManager.Instance.Toggle();
        }

        void ToggleUI(bool paused)
        {
            panel.SetActive(paused);
        }

        void OnDestroy()
        {
            if (PauseManager.Instance != null)
                PauseManager.Instance.OnPauseChanged -= ToggleUI;
        }
    }
}