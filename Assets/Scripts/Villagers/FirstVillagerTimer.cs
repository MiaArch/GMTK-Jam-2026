using System;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using Utils;

namespace Villagers
{
    public class FirstVillagerTimer: Timer
    {
        private bool hasHappened = false;
        [TextArea]
        [SerializeField] private string firstDialogue;
        public void Update()
        {
            if (Finished && !hasHappened)
            {
                hasHappened = true;
                VillagerManager.Instance.RemovePopulation(1);
                DialogueManager.Instance.AddDialogue(firstDialogue);
                //TODO: TRIGGER CUTSCENE STUFF TOO
            }
            else
            {
                if (!hasHappened)
                {
                    Tick(Time.deltaTime);
                }
            }
        }
    }
}