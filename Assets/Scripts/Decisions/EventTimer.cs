using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Decisions
{
    public class EventTimer: Timer
    {
        private bool isInEvent;
        [SerializeField] private List<DecisionEvent> events;
        private int totalEventsOccurred;

        public void Update()
        {
            if (Finished && !isInEvent)
            {
                isInEvent = true;
                DecisionCardManager.Instance.ShowEvent(events[totalEventsOccurred]);
                totalEventsOccurred++;
                //TODO: START AN EVENT, PAUSE TIMER UNTIL EVENT IS OVER, RESUME TIMER
            }
            else if (!Finished)
            {
                Tick(Time.deltaTime);
            }
        }
    }
}