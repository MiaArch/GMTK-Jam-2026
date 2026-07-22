namespace Utils
{
    using UnityEngine;

    public class Timer
    {
        float duration;
        float elapsed;

        public bool Finished => elapsed >= duration;
        public float Normalised => Mathf.Clamp01(elapsed / duration);

        public Timer(float duration)
        {
            this.duration = duration;
        }

        public void Tick(float deltaTime)
        {
            elapsed += deltaTime;
        }

        public void Reset()
        {
            elapsed = 0;
        }
    }
}