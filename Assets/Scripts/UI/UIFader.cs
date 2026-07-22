using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIFader : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] float duration = 0.5f;

        public IEnumerator FadeOut()
        {
            yield return Fade(0, 1);
        }

        public IEnumerator FadeIn()
        {
            yield return Fade(1, 0);
        }

        IEnumerator Fade(float from, float to)
        {
            float t = 0;

            while (t < duration)
            {
                t += Time.unscaledDeltaTime;

                var c = image.color;
                c.a = Mathf.Lerp(from, to, t / duration);
                image.color = c;

                yield return null;
            }
        }
    }
}