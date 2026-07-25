using System;
using System.Collections.Generic;
using Dialogue;
using Resource;
using UnityEngine;
using Utils;
using Villagers;

namespace Decisions
{
    public class EventTimer : Timer
    {
        public bool isInEvent;

        [SerializeField] private List<DecisionEvent> events;
        [SerializeField] private float firstEventOffset = 10f;
        [SerializeField] private int recurringFrequency = 5;

        // private int totalStoryEvents = 10; // Change later
        private int currentStoryIndex;
        private int storyEventsCompleted;
        private bool lastEventRecurring;

        private void Start()
        {
            elapsed = firstEventOffset;
            storyEventsCompleted = 0;
        }

        private void Update()
        {
            if (isInEvent)
                return;
            
            
            Tick(Time.deltaTime);
            if (DialogueManager.Instance.isDialogueActive)
                return;

            if (!Finished)
                return;

            DecisionEvent next = GetNextEvent();

            if (next != null)
                TriggerEvent(next);
            else GameEndings.Instance.DecideEnding();
        }

        private DecisionEvent GetNextEvent()
        {
            foreach (DecisionEvent evt in events)
            {
                if (evt.eventType != EventType.Emergency)
                    continue;

                if (!CanTrigger(evt))
                    continue;

                if (Time.time < evt.lastTriggeredTime + evt.cooldown)
                    continue;

                float value = ResourceManager.Instance.GetAmount(evt.watchedResource);

                if (value <= evt.emergencyThreshold)
                {
                    evt.lastTriggeredTime = Time.time;
                    lastEventRecurring = false;
                    return evt;
                }
            }
            
            if (storyEventsCompleted > 0 &&
                storyEventsCompleted % recurringFrequency == 0 && !lastEventRecurring)
            {
                foreach (DecisionEvent evt in events)
                {
                    if (evt.eventType != EventType.Recurring)
                        continue;

                    if (!CanTrigger(evt))
                        continue;
                    lastEventRecurring = true;
                    return evt;
                }
            }
            
            int storyCounter = 0;

            foreach (DecisionEvent evt in events)
            {
                Debug.Log($"Story index = {currentStoryIndex}, considering {storyCounter}: {evt.title}");
                if (evt.eventType != EventType.Story)
                    continue;

                if (storyCounter++ != currentStoryIndex)
                    continue;

                if (!CanTrigger(evt))
                {
                    Debug.Log($"Skipping story event: {evt.title}");
                    currentStoryIndex++;
                    continue;
                }
                    

                currentStoryIndex++;
                storyEventsCompleted++;
                lastEventRecurring = false;

                return evt;
            }

            return null;
        }

        private bool CanTrigger(DecisionEvent evt)
        {
            foreach (String flag in evt.requiredFlags)
            {
                if (!DecisionFlags.Instance.Has(flag))
                    return false;
            }

            foreach (String flag in evt.blockedFlags)
            {
                if (DecisionFlags.Instance.Has(flag))
                    return false;
            }

            return true;
        }

        private void TriggerEvent(DecisionEvent evt)
        {
            isInEvent = true;
            DecisionCardManager.Instance.ShowEvent(evt);

            
        }

        public override void Reset()
        {
            elapsed = 0f;
            isInEvent = false;
        }
    }
}