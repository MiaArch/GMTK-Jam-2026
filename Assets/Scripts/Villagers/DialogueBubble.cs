using TMPro;
using UnityEngine;

namespace Villagers
{
    public class DialogueBubble : MonoBehaviour
    {
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private Vector3 offset = new(0, 1.4f, 0);

        private Transform target;

        public void Show(Transform followTarget, string text, float duration)
        {
            target = followTarget;
            dialogueText.text = text;

            Destroy(gameObject, duration);
        }

        private void LateUpdate()
        {
            if (target == null)
                return;

            transform.position = target.position + offset;
        }
    }
}