using Dialogue;
using Villagers;

namespace Utils
{
    using UnityEngine;

    public abstract class Timer: MonoBehaviour
    {
        [SerializeField] protected float duration;
        protected float elapsed;

        protected bool Finished => elapsed >= duration;
        public float Normalised => Mathf.Clamp01(elapsed / duration);

        protected void Tick(float deltaTime)
        {
            // We may want to change this later so it pauses specific timers, but for now it's fine
            if (DialogueManager.Instance.isDialogueActive) return;
            if (GameEndings.Instance.hasEnded) return;
            elapsed += deltaTime;
        }

        public virtual void Reset()
        {
            elapsed = 0;
        }

        public void ChangeDuration(float newDuration)
        {
            duration = newDuration;
        }
    }
}