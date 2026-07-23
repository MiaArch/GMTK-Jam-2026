using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Lose Emotion")]
    public class LoseEmotionEffect: DecisionEffect
    {
        [SerializeField] private int amount;

        public override void Execute()
        {
            ResourceManager.Instance.RemoveEmotion(amount);
        }
    }
}