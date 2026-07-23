using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

namespace Decisions
{
    public class DecisionCardManager : Singleton<DecisionCardManager>
    {
        [SerializeField] private TMP_Text eventTitle;

        [SerializeField] private Transform cardContainer;
        [SerializeField] private DecisionCard cardPrefab;
        [SerializeField] private EventTimer eventTimer;
        [SerializeField] private DecisionEvent testingEvent;

        private readonly List<DecisionCard> activeCards = new();

        // public void Start()
        // {
        //     ShowEvent(testingEvent);
        // }

        public void ShowEvent(DecisionEvent decisionEvent)
        {
            eventTitle.text = decisionEvent.title;

            foreach (DecisionData choice in decisionEvent.choices)
            {
                DecisionCard card = Instantiate(cardPrefab, cardContainer);
                card.Setup(choice);

                activeCards.Add(card);
            }
        }

        public void SelectChoice(DecisionData choice)
        {
            foreach (DecisionEffect effect in choice.effects)
            {
                effect.Execute();
            }

            ClearCards();
        }

        private void ClearCards()
        {
            foreach (DecisionCard card in activeCards)
            {
                Destroy(card.gameObject);
            }

            activeCards.Clear();
            eventTimer.Reset();
        }
    }
}