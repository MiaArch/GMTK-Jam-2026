using TMPro;
using UnityEngine;

namespace Utils
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] TMP_Text label;

        void Update()
        {
            float fps = 1f / Time.unscaledDeltaTime;
            label.text = $"{fps:0}";
        }
    }
}