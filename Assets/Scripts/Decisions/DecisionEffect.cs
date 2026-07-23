using UnityEngine;


namespace Decisions
{
    public abstract class DecisionEffect : ScriptableObject
    {
        public abstract void Execute();
    }
}