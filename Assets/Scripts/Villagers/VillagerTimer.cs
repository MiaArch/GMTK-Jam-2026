using UnityEngine;
using Utils;

namespace Villagers
{
    public class VillagerTimer: Timer
    {
        public void Update()
        {
            if (Finished)
            {
                VillagerManager.Instance.RemovePopulation(1);
                Reset();
            }
            Tick(Time.deltaTime);
        }
    }
}