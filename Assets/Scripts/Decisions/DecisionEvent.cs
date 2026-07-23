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
    }
}