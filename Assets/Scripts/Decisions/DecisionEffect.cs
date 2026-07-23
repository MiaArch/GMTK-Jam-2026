using UnityEngine;


namespace Decisions
{
    public abstract class DecisionEffect : ScriptableObject
    {
        public string EffectDescription;
        public bool isPositive; // Used to determine if the text is red or green
        public DecisionEffectType DecisionEffectType; // Used to determine what icon it has
        public abstract void Execute();
    }
}