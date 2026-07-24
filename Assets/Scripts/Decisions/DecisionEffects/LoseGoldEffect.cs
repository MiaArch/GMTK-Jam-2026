using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Lose Gold")]
    public class LoseGoldEffect: DecisionEffect
    {
        public override void Execute()
        {
            ResourceManager.Instance.RemoveGold(amount);
        }
    }
}