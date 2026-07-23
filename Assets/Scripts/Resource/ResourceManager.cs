using System;
using TMPro;
using UnityEngine;
using Utils;
using Math = Unity.Mathematics.Geometry.Math;

namespace Resource
{
    public class ResourceManager: Singleton<ResourceManager>
    {
        private int buildingMaterialCount = 100;
        private int foodCount = 100;
        private int goldCount = 100;
        private int emotionCount = 100;

        [SerializeField] private TMP_Text buildingMaterialText;
        [SerializeField] private TMP_Text foodText;
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text emotionText;

        public void Start()
        {
            UpdateUI();
        }

        public void ConsumeFood()
        {
            //TODO: Revisit later
            int population = Villagers.VillagerManager.Instance.population;
            float populationRatio = population / 1000f;

            float drain =
                Mathf.Pow(populationRatio, 1.75f) * 10f;

            foodCount -= Mathf.CeilToInt(drain);
            foodCount = Mathf.Max(foodCount, 0);

            UpdateUI();
        }
        
        private void UpdateUI()
        {
            buildingMaterialText.text = buildingMaterialCount.ToString();
            foodText.text = foodCount.ToString();
            goldText.text = goldCount.ToString();
            emotionText.text = emotionCount.ToString();
        }

        public void AddFood(int amount)
        {
            foodCount = Mathf.Min(foodCount + amount, 1000);
            UpdateUI();
        }

        public void RemoveFood(int amount)
        {
            foodCount = Mathf.Max(0, foodCount - amount);
            UpdateUI();
        }
        
        public void AddWood(int amount)
        {
            buildingMaterialCount = Mathf.Min(buildingMaterialCount + amount, 1000);
            UpdateUI();
        }

        public void RemoveWood(int amount)
        {
            buildingMaterialCount = Mathf.Max(0, buildingMaterialCount - amount);
            UpdateUI();
        }

        public void AddGold(int amount)
        {
            goldCount = Mathf.Min(goldCount + amount, 1000);
            UpdateUI();
        }

        public void RemoveGold(int amount)
        {
            goldCount = Mathf.Max(0, goldCount - amount);
            UpdateUI();
        }
        
        public void AddEmotion(int amount)
        {
            emotionCount = Mathf.Min(emotionCount + amount, 1000);
            UpdateUI();
        }

        public void RemoveEmotion(int amount)
        {
            emotionCount = Mathf.Max(0, emotionCount - amount);
            UpdateUI();
        }
    }
}