using System.Collections;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] TMP_Text label;

        private float fps;
        private bool waiting;
        private bool isActive;

        public void toggleFPSCounter()
        {
            label.gameObject.SetActive(isActive);
        }
        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                isActive = !isActive;
                toggleFPSCounter();
            }
            if (!isActive)
            {
                return;
            }
            fps = 1f / Time.unscaledDeltaTime;
            if (!waiting)
            {
                waiting = true;
                StartCoroutine(updateFPSCounter());
            }
            
        }

        IEnumerator updateFPSCounter()
        {
            label.text ="FPS: " + $"{fps:0}";
            yield return new WaitForSeconds(1);
            waiting = false;
        }
    }
}