using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Lose Gold")]
    public class LoseGoldEffect: DecisionEffect
    {
        [SerializeField] private int amount;

        public override void Execute()
        {
            ResourceManager.Instance.RemoveGold(amount);
        }
    }
}