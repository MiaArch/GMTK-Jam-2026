using UnityEngine;
using Utils;

namespace Resource
{
    public class ResourceTimer: Timer
    {
        public void Update()
        {
            if (Finished)
            {
                ResourceManager.Instance.ConsumeFood();
                Reset();
            }
            Tick(Time.deltaTime);
        }
    }
}