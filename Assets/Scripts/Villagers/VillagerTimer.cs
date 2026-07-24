using System;
using Resource;
using TMPro;
using UnityEngine;
using Utils;

namespace Villagers
{
    public class VillagerTimer: Timer
    {
        [SerializeField] private float startDelay;
        [SerializeField] private float foodSurplusRequirement = 2f;
        [SerializeField] private float emotionRequirement = 100f;
        [SerializeField] private float goldRequirement = 250f;
        [SerializeField] private TMP_Text populationChangeText;
        public void Start()
        {
            elapsed = -startDelay; // Delays start of population countdown
        }

        public void Update()
        {
            if (Finished)
            {
                int populationChange = CalculatePopulationChange();
                if (populationChange > 0)
                {
                    populationChangeText.color = new Color(0, 50, 0);
                    populationChangeText.text = "+" + (Mathf.Round((populationChange / duration) * 100) / 100) + "/s";
                    VillagerManager.Instance.AddPopulation(populationChange);
                }
                else if (populationChange == 0)
                {
                    populationChangeText.text = "";
                }
                else
                {
                    populationChange *= -1; // Because it's already negative and we're removing a negative
                    populationChangeText.color = new Color(50, 0, 0);
                    populationChangeText.text = "-" + (Mathf.Round((populationChange / duration) * 100) / 100) + "/s";
                    VillagerManager.Instance.RemovePopulation(populationChange);
                }
                Reset();
            }
            Tick(Time.deltaTime);
        }

        private int CalculatePopulationChange()
        {
            int foodCount = ResourceManager.Instance.GetAmount(ResourceType.Food);
            int emotionCount = ResourceManager.Instance.GetAmount(ResourceType.Emotion);
            int goldCount = ResourceManager.Instance.GetAmount(ResourceType.Gold);
            
            float requiredFood = Mathf.Max(1, Mathf.FloorToInt(
                VillagerManager.Instance.population * ResourceManager.Instance.foodConsumedPerVillager));

            float foodRatio = foodCount / requiredFood;
            float foodScore = Mathf.Clamp01(foodRatio / foodSurplusRequirement);
            float emotionScore = Mathf.Clamp01(emotionCount / emotionRequirement);
            float goldScore = Mathf.Clamp01(goldCount / goldRequirement);
            
            float score =
                foodScore * 0.45f +
                emotionScore * 0.5f +
                goldScore * 0.05f;
            
            if (emotionCount >= emotionRequirement * 2.25) score += 0.1f;
            if (goldCount >= goldRequirement * 2) score += 0.1f;
            
            float growthRate = (score - 0.6f) * 0.06f;
            

            int populationChange = Mathf.FloorToInt(
                VillagerManager.Instance.population * growthRate);
            
            // THEY'RE STARVING!!!
            if (foodRatio < 1f)
            {
                populationChange -= Mathf.CeilToInt(
                    VillagerManager.Instance.population * (1f - foodRatio) * 0.1f);
            }
            
            return populationChange;

        }
    }
}