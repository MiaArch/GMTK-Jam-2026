using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Decisions
{
    public class EventTimer: Timer
    {
        public bool isInEvent;
        [SerializeField] private List<DecisionEvent> events;
        private int totalEventsOccurred;

        public void Start()
        {
            elapsed = 15f; //TODO: REMOVE MAGIC NUMBER WITH ACTUAL LOGIC
        }

        public void Update()
        {
            switch (Finished)
            {
                case true when !isInEvent:
                {
                    isInEvent = true;
                    if (totalEventsOccurred == events.Count) return;
                    DecisionCardManager.Instance.ShowEvent(events[totalEventsOccurred]);
                    totalEventsOccurred++;
                    break;
                }
                case false:
                    Tick(Time.deltaTime);
                    break;
            }
        }

        public override void Reset()
        {
            elapsed = 0;
            isInEvent = false;
        }
    }
}