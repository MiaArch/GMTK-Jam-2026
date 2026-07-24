using System;
using UnityEngine;
using Utils;

namespace Villagers
{
    public class FirstVillagerTimer: Timer
    {
        private bool hasHappenned = false;
        public void Update()
        {
            if (Finished && !hasHappenned)
            {
                hasHappenned = true;
                VillagerManager.Instance.RemovePopulation(1);
                //TODO: TRIGGER CUTSCENE STUFF TOO
            }
            else
            {
                if (!hasHappenned)
                {
                    Tick(Time.deltaTime);
                }
            }
        }
    }
}