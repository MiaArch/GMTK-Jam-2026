using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        public void Restart()
        {
            AudioManager.Instance.PlayButtonClick();
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}