using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}