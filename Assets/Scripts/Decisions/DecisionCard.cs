using Dialogue;
using Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Decisions
{
    public class DecisionCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Transform positiveEffectsContainer;
        [SerializeField] private Transform negativeEffectsContainer;
        [SerializeField] private GameObject effectPrefab;

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

                if (!effect.isPositive && !CanAfford(effect))
                {
                    canSelect = false;
                }

                Sprite icon = null;

                switch (effect.DecisionEffectType)
                {
                    case DecisionEffectType.Population:
                        effect.EffectDescription = (effect.isPositive ? "+" : "-") + effect.amount + " Villagers";
                        icon = VillagerSprite;
                        break;

                    case DecisionEffectType.Wood:
                        effect.EffectDescription = (effect.isPositive ? "+" : "-") + effect.amount + " Materials";
                        icon = WoodSprite;
                        break;

                    case DecisionEffectType.Food:
                        effect.EffectDescription = (effect.isPositive ? "+" : "-") + effect.amount + " Food";
                        icon = FoodSprite;
                        break;

                    case DecisionEffectType.Gold:
                        effect.EffectDescription = (effect.isPositive ? "+" : "-") + effect.amount + " Gold";
                        icon = GoldSprite;
                        break;

                    case DecisionEffectType.Emotion:
                        effect.EffectDescription = (effect.isPositive ? "+" : "-") + effect.amount + " Happiness";
                        icon = EmotionSprite;
                        break;

                    case DecisionEffectType.Clues:
                        effect.EffectDescription = (effect.isPositive ? "+" : "-") + effect.amount + " Clue";
                        icon = ClueSprite;
                        break;

                    case DecisionEffectType.Lumbermill:
                        effect.EffectDescription = "+" + effect.amount + " Materials /s";
                        icon = WoodSprite;
                        break;

                    case DecisionEffectType.Farm:
                        effect.EffectDescription = "+" + effect.amount + " Food /s";
                        icon = FoodSprite;
                        break;
                }


                GameObject row = Instantiate(effectPrefab);

                row.transform.SetParent(
                    effect.isPositive 
                        ? positiveEffectsContainer 
                        : negativeEffectsContainer,
                    false
                );

                row.GetComponent<EffectRow>().Setup(
                    icon,
                    effect.EffectDescription,
                    effect.isPositive
                );
            }


            positiveToDisable.SetActive(positiveEffectsContainer.childCount > 0);
            negativeToDisable.SetActive(negativeEffectsContainer.childCount > 0);

            cardBackground.color = canSelect ? Color.white : disabledTint;

            titleText.text = data.title;
            descriptionText.text = data.description;
        }

        public void SelectCard()
        {
            AudioManager.Instance.PlayButtonClick();
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