using UnityEngine;
using Villagers;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Gain Population")]
    public class GainPopulationEffect : DecisionEffect
    {
        public override void Execute()
        {
            VillagerManager.Instance.AddPopulation(amount);
        }
    }
}