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