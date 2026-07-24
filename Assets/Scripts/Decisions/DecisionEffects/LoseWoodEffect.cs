using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Lose Wood")]
    public class LoseWoodEffect: DecisionEffect
    {

        public override void Execute()
        {
            ResourceManager.Instance.RemoveWood(amount);
        }
    }
}