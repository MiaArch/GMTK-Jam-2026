using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Lose Emotion")]
    public class LoseEmotionEffect: DecisionEffect
    {
        public override void Execute()
        {
            ResourceManager.Instance.RemoveEmotion(amount);
        }
    }
}