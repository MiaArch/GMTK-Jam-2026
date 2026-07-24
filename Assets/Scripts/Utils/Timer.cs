using Dialogue;

namespace Utils
{
    using UnityEngine;

    public abstract class Timer: MonoBehaviour
    {
        [SerializeField] protected float duration;
        protected float elapsed;

        public bool Finished => elapsed >= duration;
        public float Normalised => Mathf.Clamp01(elapsed / duration);

        public void Tick(float deltaTime)
        {
            // We may want to change this later so it pauses specific timers, but for now it's fine
            if (DialogueManager.Instance.isDiaglogueActive != true)
            {
                elapsed += deltaTime;
            }
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