using UnityEngine;

namespace Villagers
{
    public class BubbleSpawner : MonoBehaviour
    {
        public static BubbleSpawner Instance;

        [SerializeField]
        private DialogueBubble bubblePrefab;

        private void Awake()
        {
            Instance = this;
        }

        public DialogueBubble ShowBubble(Transform target, string text, float duration = 2.75f)
        {
            DialogueBubble bubble = Instantiate(bubblePrefab);
            bubble.Show(target, text, duration);
            return bubble;
        }
    }
}