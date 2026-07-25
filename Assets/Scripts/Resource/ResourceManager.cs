using TMPro;
using UnityEngine;
using Utils;
using Villagers;


namespace Resource
{
    public class ResourceManager: Singleton<ResourceManager>
    {
        [SerializeField] private float woodCount = 100;
        [SerializeField] private float foodCount = 100;
        [SerializeField] private float goldCount = 100;
        [SerializeField] private float emotionCount = 100;
        private int cluesCount;

        private int maxFood = 9999;
        private int maxGold = 9999;
        private int maxEmotion = 1000;
        private int maxWood = 9999;

        public int woodPerSecond;
        public int foodPerSecond;

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

        public float[] GainResource(float duration)
        {
            float woodGain = woodPerSecond * duration;
            float foodGain = foodPerSecond * duration;
            woodCount = Mathf.Max(0, woodCount + woodGain);
            foodCount = Mathf.Max(0, foodCount + foodGain);
            UpdateUI();

            return new[] { woodGain, foodGain };
            
        }
        
        private void UpdateUI()
        {
            // ReSharper disable SpecifyACultureInStringConversionExplicitly
            buildingMaterialText.text = woodCount >= maxWood ? woodCount + "[MAX]" : Mathf.FloorToInt(woodCount).ToString();
            foodText.text = foodCount >= maxFood ? foodCount + "[MAX]": Mathf.FloorToInt(foodCount).ToString();
            goldText.text = goldCount >= maxGold ? goldCount + "[MAX]" : Mathf.FloorToInt(goldCount).ToString();
            emotionText.text = emotionCount >= maxEmotion ? emotionCount + "[MAX]" : Mathf.FloorToInt(emotionCount).ToString();
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

        public void AddFoodPerSecond(int amount)
        {
            foodPerSecond += amount;
        }

        public void AddWoodPerSecond(int amount)
        {
            woodPerSecond += amount;
        }

        public int GetAmount(ResourceType resource)
        {
            switch (resource)
            {
                case ResourceType.Food:
                    return Mathf.FloorToInt(foodCount);
                case ResourceType.Wood:
                    return Mathf.FloorToInt(woodCount);
                case ResourceType.Gold:
                    return Mathf.FloorToInt(goldCount);
                case ResourceType.Emotion:
                    return Mathf.FloorToInt(emotionCount);
                case ResourceType.Clues:
                    return cluesCount;
                default:
                    return 0;
            }
        }
    }
}