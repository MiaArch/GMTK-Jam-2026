namespace Utils
{
    using UnityEngine;

    public abstract class Timer: MonoBehaviour
    {
        [SerializeField] private float duration;
        private float elapsed;

        public bool Finished => elapsed >= duration;
        public float Normalised => Mathf.Clamp01(elapsed / duration);

        public Timer(float duration)
        {
            this.duration = duration;
        }

        public void Tick(float deltaTime)
        {
            elapsed += deltaTime;
            Debug.Log(elapsed);
        }

        public void Reset()
        {
            elapsed = 0;
        }

        public void ChangeDuration(float newDuration)
        {
            duration = newDuration;
        }
    }
}