using System.Collections.Generic;
using Resource;
using UnityEngine;

namespace Decisions
{
    [CreateAssetMenu(menuName = "Game/Decision Event")]
    public class DecisionEvent : ScriptableObject
    {
        public string title;

        [TextArea]
        public string description;

        public DecisionData[] choices;
        public EventType eventType;
        
        public List<string> requiredFlags;
        public List<string> blockedFlags;
        
        [Header("Emergency")]
        public ResourceType watchedResource;
        public float emergencyThreshold;
        public float cooldown = 60f;
        
        
        [HideInInspector] public float lastTriggeredTime = -999f;
    }
}