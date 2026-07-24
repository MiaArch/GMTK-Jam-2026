using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Lose Food")]
    public class LoseFoodEffect: DecisionEffect
    {
        public override void Execute()
        {
            ResourceManager.Instance.RemoveFood(amount);
        }
    }
}