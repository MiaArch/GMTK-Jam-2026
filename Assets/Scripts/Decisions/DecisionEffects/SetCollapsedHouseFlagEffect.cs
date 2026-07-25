using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Set Collapsed House Flag")]
    public class SetCollapsedHouseFlagEffect: DecisionEffect
    {
        public override void Execute()
        {
            DecisionFlags.Instance.Set("CollapsedHouse");
        }
    }
}