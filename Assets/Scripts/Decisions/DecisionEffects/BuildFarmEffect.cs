using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Build Farm Effect")]
    public class BuildFarmEffect: DecisionEffect
    {
        public override void Execute()
        {
            EffectDescription = "+" + amount + "/s";
            ResourceManager.Instance.AddFoodPerSecond(amount);
        }
    }
}