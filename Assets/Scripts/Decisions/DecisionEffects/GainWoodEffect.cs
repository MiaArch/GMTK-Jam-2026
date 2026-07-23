using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Gain Wood")]
    public class GainWoodEffect: DecisionEffect
    {
        [SerializeField] private int amount;

        public override void Execute()
        {
            ResourceManager.Instance.AddWood(amount);
        }
    }
}