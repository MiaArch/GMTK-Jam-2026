using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Decisions
{
    public class DecisionCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text positiveEffectsText;
        [SerializeField] private TMP_Text negativeEffectsText;
        [SerializeField] private Image positiveEffectsImage;
        [SerializeField] private Image negativeEffectsImage;

        [SerializeField] private GameObject positiveToDisable;
        [SerializeField] private GameObject negativeToDisable;

        [Header("Image References")] 
        [SerializeField] private Sprite VillagerSprite;
        [SerializeField] private Sprite WoodSprite;
        [SerializeField] private Sprite FoodSprite;
        [SerializeField] private Sprite GoldSprite;
        [SerializeField] private Sprite EmotionSprite;
        [SerializeField] private Sprite ClueSprite;

        private DecisionData decision;
        public void Setup(DecisionData data)
        {
            decision = data;
            foreach (var effect in data.effects)
            {
                if (effect.isPositive) positiveEffectsText.text += effect.EffectDescription + "\n";
                else negativeEffectsText.text += effect.EffectDescription + "\n";

                switch (effect.DecisionEffectType)
                {
                    case DecisionEffectType.Population:
                        if (effect.isPositive) positiveEffectsImage.sprite = VillagerSprite;
                        else negativeEffectsImage.sprite = VillagerSprite;
                        break;
                    case DecisionEffectType.Wood:
                        if (effect.isPositive) positiveEffectsImage.sprite = WoodSprite;
                        else negativeEffectsImage.sprite = WoodSprite;
                        break;
                    case DecisionEffectType.Food:
                        if (effect.isPositive) positiveEffectsImage.sprite = FoodSprite;
                        else negativeEffectsImage.sprite = FoodSprite;
                        break;
                    case DecisionEffectType.Gold:
                        if (effect.isPositive) positiveEffectsImage.sprite = GoldSprite;
                        else negativeEffectsImage.sprite = GoldSprite;
                        break;
                    case DecisionEffectType.Emotion:
                        if (effect.isPositive) positiveEffectsImage.sprite = EmotionSprite;
                        else negativeEffectsImage.sprite = EmotionSprite;
                        break;
                    case DecisionEffectType.Clues:
                        if (effect.isPositive) positiveEffectsImage.sprite = ClueSprite;
                        else negativeEffectsImage.sprite = ClueSprite;
                        break;
                    default:
                        if (effect.isPositive) positiveEffectsImage.sprite = VillagerSprite;
                        else negativeEffectsImage.sprite = VillagerSprite;
                        break;
                }
            }

            if (positiveEffectsText.text == "") positiveToDisable.SetActive(false);
            if (negativeEffectsText.text == "") negativeToDisable.SetActive(false);

            titleText.text = data.title;
            descriptionText.text = data.description;
        }

        public void SelectCard()
        {
            if (decision.hasDialogue)
            {
                DialogueManager.Instance.AddDialogue(decision.dialogueLines);
            }
            Debug.Log($"Selected: {decision.title}");
            DecisionCardManager.Instance.SelectChoice(decision);
        }
    }
}