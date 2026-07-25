using UnityEngine;
using Utils;

namespace UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] string sceneName;

        public void LoadScene()
        {
            AudioManager.Instance.PlayButtonClick();
            Time.timeScale = 1f;
            SceneLoader.Instance.Load(sceneName);
        }
    }
}