using TMPro;
using UnityEngine;
using Utils;
using Villagers;


namespace Resource
{
    public class ResourceManager: Singleton<ResourceManager>
    {
        [SerializeField] private int woodCount = 100;
        [SerializeField] private int foodCount = 100;
        [SerializeField] private int goldCount = 100;
        [SerializeField] private int emotionCount = 100;
        private int cluesCount = 0;

        private int maxFood = 9999;
        private int maxGold = 9999;
        private int maxEmotion = 1000;
        private int maxWood = 9999;

        [SerializeField] public float foodConsumedPerVillager = 0.05f;
        [SerializeField] private TMP_Text buildingMaterialText;
        [SerializeField] private TMP_Text foodText;
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text emotionText;

        public void Start()
        {
            UpdateUI();
        }

        public int ConsumeFood()
        {
            int population = VillagerManager.Instance.population;
            int foodToConsume = Mathf.Max(1, Mathf.FloorToInt(population * foodConsumedPerVillager));
            foodCount = Mathf.Max(0, foodCount - foodToConsume);
            UpdateUI();
            return foodToConsume;
            
        }
        
        private void UpdateUI()
        {
            buildingMaterialText.text = woodCount == maxWood ? woodCount + "[MAX]" : woodCount.ToString() ;
            foodText.text = foodCount == maxFood ? foodCount + "[MAX]": foodCount.ToString();
            goldText.text = goldCount == maxGold ? goldCount + "[MAX]" : goldCount.ToString();
            emotionText.text = emotionCount == maxEmotion ? emotionCount + "[MAX]" : emotionCount.ToString();
        }

        public void AddFood(int amount)
        {
            foodCount = Mathf.Min(foodCount + amount, maxFood);
            UpdateUI();
        }

        public void RemoveFood(int amount)
        {
            foodCount = Mathf.Max(0, foodCount - amount);
            UpdateUI();
        }
        
        public void AddWood(int amount)
        {
            woodCount = Mathf.Min(woodCount + amount, maxWood);
            UpdateUI();
        }

        public void RemoveWood(int amount)
        {
            woodCount = Mathf.Max(0, woodCount - amount);
            UpdateUI();
        }

        public void AddGold(int amount)
        {
            goldCount = Mathf.Min(goldCount + amount, maxGold);
            UpdateUI();
        }

        public void RemoveGold(int amount)
        {
            goldCount = Mathf.Max(0, goldCount - amount);
            UpdateUI();
        }
        
        public void AddEmotion(int amount)
        {
            emotionCount = Mathf.Min(emotionCount + amount, maxEmotion);
            UpdateUI();
        }

        public void RemoveEmotion(int amount)
        {
            emotionCount = Mathf.Max(0, emotionCount - amount);
            UpdateUI();
        }

        public void AddClue(int amount)
        {
            cluesCount += amount;
        }

        public int GetAmount(ResourceType resource)
        {
            switch (resource)
            {
                case ResourceType.Food:
                    return foodCount;
                case ResourceType.Wood:
                    return woodCount;
                case ResourceType.Gold:
                    return goldCount;
                case ResourceType.Emotion:
                    return emotionCount;
                case ResourceType.Clues:
                    return cluesCount;
                default:
                    return 0;
            }
        }
    }
}