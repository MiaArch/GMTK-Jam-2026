using UnityEngine;


namespace Decisions
{
    public abstract class DecisionEffect : ScriptableObject
    {
        public bool isHidden; // These events won't show to the player...
        public bool isPositive; // Used to determine if the text is red or green
        public DecisionEffectType DecisionEffectType; // Used to determine what icon it has
        public int amount;
        [HideInInspector] public string EffectDescription;
        public abstract void Execute();
    }
}