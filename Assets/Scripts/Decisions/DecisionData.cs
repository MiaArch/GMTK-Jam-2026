using UnityEngine;

namespace Decisions
{
    [CreateAssetMenu(fileName = "Decision", menuName = "Game/Decision")]
    public class DecisionData : ScriptableObject
    {
        [Header("Display")]
        public string title;
        [TextArea(4, 8)]
        public string description;

        public DecisionEffect[] effects;
    }
}