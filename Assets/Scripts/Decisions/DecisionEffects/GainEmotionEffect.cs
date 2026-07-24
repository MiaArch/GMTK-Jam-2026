using Resource;
using UnityEngine;

namespace Decisions.DecisionEffects
{
    [CreateAssetMenu(menuName = "Decision Effects/Gain Emotion")]
    public class GainEmotionEffect: DecisionEffect
    {
        public override void Execute()
        {
            ResourceManager.Instance.AddEmotion(amount);
        }
    }
}