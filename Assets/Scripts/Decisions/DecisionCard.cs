using TMPro;
using UnityEngine;

namespace Decisions
{
    public class DecisionCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;

        private DecisionData decision;

        public void Setup(DecisionData data)
        {
            decision = data;

            titleText.text = data.title;
            descriptionText.text = data.description;
        }

        public void SelectCard()
        {
            Debug.Log($"Selected: {decision.title}");
            DecisionCardManager.Instance.SelectChoice(decision);
        }
    }
}