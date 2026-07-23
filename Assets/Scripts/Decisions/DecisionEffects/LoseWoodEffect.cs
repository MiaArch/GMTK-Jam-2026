using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Lose Wood")]
    public class LoseWoodEffect: DecisionEffect
    {
        [SerializeField] private int amount;

        public override void Execute()
        {
            ResourceManager.Instance.RemoveWood(amount);
        }
    }
}