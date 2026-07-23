using TMPro;
using UnityEngine;

namespace Decisions
{
    public class DecisionCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text positiveEffectsText;
        [SerializeField] private TMP_Text negativeEffectsText;

        private DecisionData decision;
        public void Setup(DecisionData data)
        {
            decision = data;
            for (int i = 0; i < data.effects.Length ; i++)
            {
                DecisionEffect effect = data.effects[i];
                if (effect.isPositive)
                {
                    positiveEffectsText.text += effect.EffectDescription;
                }
                else negativeEffectsText.text += effect.EffectDescription;
            }
            
            if (positiveEffectsText.text == "") positiveEffectsText.gameObject.SetActive(false);
            if (negativeEffectsText.text == "") negativeEffectsText.gameObject.SetActive(false);

            titleText.text = data.title;
            descriptionText.text = data.description;
        }

        public void SelectCard()
        {
            Debug.Log($"Selected: {decision.title}");
            DecisionCardManager.Instance.SelectChoice(decision);
        }
    }
}