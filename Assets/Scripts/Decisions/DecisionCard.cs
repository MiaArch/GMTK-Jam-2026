using Dialogue;
using Resource;
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
        
        [SerializeField] private Image cardBackground;
        [SerializeField] private Color disabledTint = new Color(0.45f, 0.4f, 0.4f, 0.95f);

        [Header("Image References")] 
        [SerializeField] private Sprite VillagerSprite;
        [SerializeField] private Sprite WoodSprite;
        [SerializeField] private Sprite FoodSprite;
        [SerializeField] private Sprite GoldSprite;
        [SerializeField] private Sprite EmotionSprite;
        [SerializeField] private Sprite ClueSprite;

        private DecisionData decision;
        private bool canSelect = true;
        public void Setup(DecisionData data)
        {
            decision = data;
            canSelect = true;
            foreach (var effect in data.effects)
            {
                if (effect.isHidden) continue;
                if (!effect.isPositive)
                {
                    if (!CanAfford(effect))
                    {
                        canSelect = false;
                    }
                }
                switch (effect.DecisionEffectType)
                {
                    case DecisionEffectType.Population:
                        if (effect.isPositive)
                        {
                            effect.EffectDescription = "+" + effect.amount + " Villagers";
                            positiveEffectsImage.sprite = VillagerSprite;
                        }
                        else
                        {
                            effect.EffectDescription = "-" + effect.amount + " Villagers";
                            negativeEffectsImage.sprite = VillagerSprite;
                        }
                        break;
                    case DecisionEffectType.Wood:
                        if (effect.isPositive)
                        {
                            effect.EffectDescription = "+" + effect.amount + " Materials";
                            positiveEffectsImage.sprite = WoodSprite;
                        }
                        else
                        {
                            effect.EffectDescription = "-" + effect.amount + " Materials";
                            negativeEffectsImage.sprite = WoodSprite;
                        }
                        break;
                    case DecisionEffectType.Food:
                        if (effect.isPositive)
                        {
                            effect.EffectDescription = "+" + effect.amount + " Food";
                            positiveEffectsImage.sprite = FoodSprite;
                        }
                        else
                        {
                            effect.EffectDescription = "-" + effect.amount + " Food";
                            negativeEffectsImage.sprite = FoodSprite;
                        }
                        break;
                    case DecisionEffectType.Gold:
                        if (effect.isPositive)
                        {
                            effect.EffectDescription = "+" + effect.amount + " Gold";
                            positiveEffectsImage.sprite = GoldSprite;
                        }
                        else
                        {
                            effect.EffectDescription = "-" + effect.amount + " Gold";
                            negativeEffectsImage.sprite = GoldSprite;
                        }
                        break;
                    case DecisionEffectType.Emotion:
                        if (effect.isPositive)
                        {
                            effect.EffectDescription = "+" + effect.amount + " Happiness";
                            positiveEffectsImage.sprite = EmotionSprite;
                        }
                        else
                        {
                            effect.EffectDescription = "-" + effect.amount + " Happiness";
                            negativeEffectsImage.sprite = EmotionSprite;
                        }
                        break;
                    case DecisionEffectType.Clues:
                        if (effect.isPositive)
                        {
                            effect.EffectDescription = "+" + effect.amount + " Clue";
                            positiveEffectsImage.sprite = ClueSprite;
                        }
                        else
                        {
                            effect.EffectDescription = "-" + effect.amount + " Clue";
                            negativeEffectsImage.sprite = ClueSprite;
                        }
                        break;
                    case DecisionEffectType.Lumbermill:
                        effect.EffectDescription = "+" + effect.amount + " Materials /s";
                        positiveEffectsImage.sprite = WoodSprite;
                        break;
                    case DecisionEffectType.Farm:
                        effect.EffectDescription = "+" + effect.amount + " Food /s";
                        positiveEffectsImage.sprite = FoodSprite;
                        break;
                    default:
                        if (effect.isPositive) positiveEffectsImage.sprite = VillagerSprite;
                        else negativeEffectsImage.sprite = VillagerSprite;
                        break;
                    
                }
                if (effect.isPositive) positiveEffectsText.text += effect.EffectDescription + "\n";
                else negativeEffectsText.text += effect.EffectDescription + "\n";
            }

            if (positiveEffectsText.text == "") positiveToDisable.SetActive(false);
            if (negativeEffectsText.text == "") negativeToDisable.SetActive(false);
            
            if (!canSelect)
            {
                cardBackground.color = disabledTint;
            }
            else
            {
                cardBackground.color = Color.white;
            }

            titleText.text = data.title;
            descriptionText.text = data.description;
        }

        public void SelectCard()
        {
            if (!canSelect)
                return;
            
            if (decision.hasDialogue)
            {
                DialogueManager.Instance.AddDialogue(decision.dialogueLines);
            }
            Debug.Log($"Selected: {decision.title}");
            DecisionCardManager.Instance.SelectChoice(decision);
        }
        
        private bool CanAfford(DecisionEffect effect)
        {
            ResourceType resourceType;

            switch (effect.DecisionEffectType)
            {
                case DecisionEffectType.Wood:
                    resourceType = ResourceType.Wood;
                    break;
                case DecisionEffectType.Food:
                    resourceType = ResourceType.Food;
                    break;
                case DecisionEffectType.Gold:
                    resourceType = ResourceType.Gold;
                    break;
                default:
                    return true;
            }

            return ResourceManager.Instance.GetAmount(resourceType) >= effect.amount;
        }
    }
}