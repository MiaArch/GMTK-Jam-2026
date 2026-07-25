using Resource;
using UnityEngine;
using UnityEngine.UI;

namespace Noia
{
    [RequireComponent(typeof(Image))]
    public class NoiaUIEffect : MonoBehaviour
    {
        [SerializeField] private int cluesForMaxDarkness = 10;
        [SerializeField, Range(0, 1)] private float maxAlpha = 0.8f;

        private Image image;
        private int lastClues = -1;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Update()
        {
            int clues = ResourceManager.Instance.GetAmount(ResourceType.Clues);

            if (clues == lastClues)
                return;

            lastClues = clues;

            Color c = image.color;
            c.a = Mathf.Clamp01((float)clues / cluesForMaxDarkness) * maxAlpha;
            image.color = c;
        }
    }
}