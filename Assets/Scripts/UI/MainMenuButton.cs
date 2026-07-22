using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] string menuScene = "MainMenu";

        public void QuitToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(menuScene);
        }
    }
}