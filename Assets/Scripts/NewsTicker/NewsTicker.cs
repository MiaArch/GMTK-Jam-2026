using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace NewsTicker
{
    public class NewsTicker : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform tickerText;
        [SerializeField] private TextMeshProUGUI textComponent;

        [Header("Settings")]
        [SerializeField] private float scrollSpeed = 150f;
        [SerializeField] private float startOffset = 20f;
        [SerializeField] private float endOffset = 20f;

        [Header("News")]
        [SerializeField] private List<NewsItem> newsItems = new();

        private float panelWidth;
        private NewsItem currentNews;

        private void Start()
        {
            panelWidth = ((RectTransform)transform).rect.width;
            ShowNextNews();
        }

        private void Update()
        {
            tickerText.anchoredPosition += Vector2.left * (scrollSpeed * Time.deltaTime);

            if (tickerText.anchoredPosition.x < -textComponent.preferredWidth - endOffset)
            {
                ShowNextNews();
            }
        }

        private void ShowNextNews()
        {
            List<NewsItem> available = GetAvailableNews();

            if (available.Count == 0)
            {
                textComponent.text = "";
                return;
            }

            currentNews = GetWeightedRandomNews(available);

            textComponent.text = currentNews.headline;

            Canvas.ForceUpdateCanvases();

            tickerText.anchoredPosition = new Vector2(panelWidth + startOffset, 0);
        }

        private List<NewsItem> GetAvailableNews()
        {
            return newsItems.Where(IsNewsValid).ToList();
        }

        private bool IsNewsValid(NewsItem item)
        {
            if (item.conditions == null || item.conditions.Count == 0)
                return true;

            foreach (var condition in item.conditions)
            {
                if (!condition.Evaluate())
                    return false;
            }

            return true;
        }

        private NewsItem GetWeightedRandomNews(List<NewsItem> available)
        {
            int totalWeight = 0;

            foreach (var item in available)
                totalWeight += Mathf.Max(1, item.weight);

            int random = Random.Range(0, totalWeight);

            foreach (var item in available)
            {
                random -= Mathf.Max(1, item.weight);

                if (random < 0)
                    return item;
            }

            return available[0];
        }
    }
}