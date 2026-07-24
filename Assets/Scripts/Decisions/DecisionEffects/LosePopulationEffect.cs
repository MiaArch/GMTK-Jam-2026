using UnityEngine;
using Villagers;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Lose Population")]
    public class LosePopulationEffect : DecisionEffect
    {

        public override void Execute()
        {
            VillagerManager.Instance.RemovePopulation(amount);
        }
    }
}