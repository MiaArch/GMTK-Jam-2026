using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace UI
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] string menuScene = "MainMenu";

        public void QuitToMenu()
        {
            AudioManager.Instance.PlayButtonClick();
            Time.timeScale = 1f;
            SceneManager.LoadScene(menuScene);
        }
    }
}