using TMPro;
using UnityEngine;
using Utils;

namespace Resource
{
    public class ResourceTimer: Timer
    {
        [SerializeField] private TMP_Text foodDrainText;
        [SerializeField] private TMP_Text woodChangeText;
        [SerializeField] private TMP_Text emotionDrainText;
        [SerializeField] private float startDelay;

        [SerializeField] private int surplusFoodHappiness = 10;

        public void Start()
        {
            elapsed = -startDelay;
        }

        public void Update()
        {
            if (Finished)
            {
                CalculateResourceChanges();
                Reset();
            }
            Tick(Time.deltaTime);
        }

        private void CalculateResourceChanges()
        {
            float[] gains = ResourceManager.Instance.GainResource(duration);
            float woodGained = gains[0];
            float foodGained = gains[1];

            float woodRate = woodGained / duration;
            
            if (woodRate > 0)
            {
                woodChangeText.text = "+" + (Mathf.Round(woodRate * 100) / 100f) + "/s";
                woodChangeText.color = new Color(0, 50, 0);
            }
            else if (woodRate < 0)
            {
                woodChangeText.text = (Mathf.Round(woodRate * 100) / 100f) + "/s";
                woodChangeText.color = new Color(50, 0, 0);
            }
            else
            {
                woodChangeText.text = "";
            }
            
            
            int foodConsumed = ResourceManager.Instance.ConsumeFood();

            float foodNetChange = foodGained - foodConsumed;
            float foodRate = foodNetChange / duration;

            if (foodRate > 0)
            {
                foodDrainText.text = "+" + (Mathf.Round(foodRate * 100) / 100f) + "/s";
                foodDrainText.color = new Color(0, 50, 0);
            }
            else if (foodRate < 0)
            {
                foodDrainText.text = (Mathf.Round(foodRate * 100) / 100f) + "/s";
                foodDrainText.color = new Color(50, 0, 0);
            }
            else
            {
                foodDrainText.text = "";
            }
            
            float foodRatio = ResourceManager.Instance.GetAmount(ResourceType.Food) / (float)foodConsumed;
            
            float foodScore = Mathf.Clamp01(foodRatio / surplusFoodHappiness);
            float goldScore = Mathf.Clamp01(ResourceManager.Instance.GetAmount(ResourceType.Gold) / 500f);

            float emotionScore = foodScore * 0.6f + goldScore * 0.4f;

            if (foodRatio >= surplusFoodHappiness *5)
            {
                emotionScore += 0.1f;
            }
            float emotionRate = (emotionScore - 0.6f) * 0.01f;
            
            int emotionChange = Mathf.RoundToInt(
                ResourceManager.Instance.GetAmount(ResourceType.Emotion) * emotionRate);

            if (emotionChange > 0)
            {
                ResourceManager.Instance.AddEmotion(emotionChange);
                emotionDrainText.text = "+" + (Mathf.Round((emotionChange / duration) * 100) / 100) + "/s";

            }
            else if (emotionChange == 0)
            {
                emotionDrainText.text = "";
            }
            else
            {
                emotionChange *= -1;
                ResourceManager.Instance.RemoveEmotion(emotionChange);
                emotionDrainText.text = "-" + (Mathf.Round((emotionChange / duration) * 100) / 100) + "/s";

            }



        }
    }
}