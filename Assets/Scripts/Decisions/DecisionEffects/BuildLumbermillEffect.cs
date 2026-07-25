using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Build Lumbermill Effect")]
    public class BuildLumbermillEffect : DecisionEffect
    {
        public override void Execute()
        {
            EffectDescription = "+" + amount + "/s";
            ResourceManager.Instance.AddWoodPerSecond(amount);
        }
    }
}