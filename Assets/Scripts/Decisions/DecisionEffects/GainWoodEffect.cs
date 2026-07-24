using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Gain Wood")]
    public class GainWoodEffect: DecisionEffect
    {
        public override void Execute()
        {
            ResourceManager.Instance.AddWood(amount);
        }
    }
}