using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Gain Food")]
    public class GainFoodEffect: DecisionEffect
    {
        public override void Execute()
        {
            ResourceManager.Instance.AddFood(amount);
        }
    }
}