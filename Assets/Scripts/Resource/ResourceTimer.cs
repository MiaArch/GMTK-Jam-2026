using TMPro;
using UnityEngine;
using Utils;

namespace Resource
{
    public class ResourceTimer: Timer
    {
        [SerializeField] private TMP_Text foodDrainText;
        [SerializeField] private TMP_Text emotionDrainText;
        [SerializeField] private float startDelay;

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
            // Later we might have farms/lumbermills that increase resources
            int foodConsumed = ResourceManager.Instance.ConsumeFood();
            foodDrainText.text = "-" + (Mathf.Round((foodConsumed / duration) * 100) / 100) + "/s";

            float foodRatio = ResourceManager.Instance.GetAmount(ResourceType.Food) / (float)foodConsumed;
            
            float foodScore = Mathf.Clamp01(foodRatio / 10);
            float goldScore = Mathf.Clamp01(ResourceManager.Instance.GetAmount(ResourceType.Gold) / 500f);

            float emotionScore = foodScore * 0.6f + goldScore * 0.4f;
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