using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneLoader : PersistentSingleton<SceneLoader>
    {
        public void Load(string scene)
        {
            StartCoroutine(LoadRoutine(scene));
        }

        IEnumerator LoadRoutine(string scene)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(scene);

            while (!op.isDone)
                yield return null;
        }

        public void Reload()
        {
            Load(SceneManager.GetActiveScene().name);
        }
    }
}