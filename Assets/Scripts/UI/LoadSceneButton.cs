using UnityEngine;
using Utils;

namespace UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] string sceneName;

        public void LoadScene()
        {
            Time.timeScale = 1f;
            SceneLoader.Instance.Load(sceneName);
        }
    }
}