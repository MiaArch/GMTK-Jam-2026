using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Gain Gold")]
    public class GainGoldEffect: DecisionEffect
    {
        public override void Execute()
        {
            ResourceManager.Instance.AddGold(amount);
        }
    }
}