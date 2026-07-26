using System;
using System.Collections;
using System.Collections.Generic;
using Decisions;
using Dialogue;
using Resource;
using TMPro;
using UnityEngine;
using Utils;

namespace Villagers
{
    /*
    *   Lonely ending (bad ending):
        Reached population of 2 (only you and your advisor)

        Paranoia ending (somewhat of a canon ending)
        Finish the story with clues exceeding a set amount. 
        You spent so much time chasing these clues, you neglected your village as a result. 

        Survival ending
        Finish the story

        Best ending
        Finish the story AND have population exceeding X amount with happiness above X amount.
     */
    public class GameEndings: Singleton<GameEndings>
    {
        public bool hasEnded;
        
        [Header("UI")]
        [SerializeField] private GameObject endingsPanel;
        [SerializeField] private TMP_Text endingTitle;
        [SerializeField] private TMP_Text endingDescription;
        [SerializeField] private TMP_Text endingScore;


        [Header("Required Ending Values")]
        [SerializeField] private int paranoiaCluesRequired;
        [SerializeField] private int populationRequirementBestEnding;
        [SerializeField] private int emotionRequirementBestEnding;

        private float score;

        [SerializeField] private List<String> doomedEndingDialogueLines;
        [SerializeField] private List<String> badEndingDialogueLines;
        [SerializeField] private List<String> paranoiaEndingDialogueLines;
        [SerializeField] private List<String> survivalEndingDialogueLines;
        [SerializeField] private List<String> bestEndingDialogueLines;

        public void Start()
        {
            endingsPanel.SetActive(false);
        }

        public void TriggerBadEnding()
        {
            StartCoroutine(BadEndingRoutine());
        }

        public void DecideEnding()
        {
            score = Mathf.FloorToInt(calculateScore());
            if (ResourceManager.Instance.GetAmount(ResourceType.Clues) >= paranoiaCluesRequired)
            {
                TriggerNoiaEnding();
                return;
            }
            if (score < 0)
            {
                TriggerDoomedEnding();
                return;
                // If someone gets this legitimately, I will question their intelligence
            }
            if (ResourceManager.Instance.GetAmount(ResourceType.Emotion) < emotionRequirementBestEnding 
                     || VillagerManager.Instance.population < populationRequirementBestEnding || ResourceManager.Instance.GetAmount(ResourceType.Clues) > 3)
            {
                TriggerSurvivalEnding();
            }
            else TriggerBestEnding();
        }

        public void TriggerDoomedEnding()
        {
            StartCoroutine(DoomedEndingRoutine());
        }

        public void TriggerNoiaEnding()
        {
            StartCoroutine(NoiaEndingRoutine());
        }

        public void TriggerSurvivalEnding()
        {
            StartCoroutine(SurvivalEndingRoutine());
        }

        public void TriggerBestEnding()
        {
            StartCoroutine(BestEndingRoutine());
        }
        
        private IEnumerator DoomedEndingRoutine()
        {
            DialogueManager.Instance.AddDialogue(doomedEndingDialogueLines);
            hasEnded = true;
            DecisionCardManager.Instance.ClearCards();
            
            yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);

            endingsPanel.SetActive(true);
            
            endingTitle.text = "Doomed Ending";
            endingDescription.text = "You survived, but surely this will be the end for your village";
            endingScore.text = "Score: " + score;
        }

        private IEnumerator BadEndingRoutine()
        {
            DialogueManager.Instance.AddDialogue(badEndingDialogueLines);
            hasEnded = true;
            DecisionCardManager.Instance.ClearCards();
            
            yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);

            endingsPanel.SetActive(true);
            
            endingTitle.text = "Lonely Ending";
            endingDescription.text = "Through your failures, you doomed your village.";
            endingScore.text = "Score: " + score;
        }
        
        private IEnumerator NoiaEndingRoutine()
        {
            DialogueManager.Instance.AddDialogue(paranoiaEndingDialogueLines);
            hasEnded = true;
            DecisionCardManager.Instance.ClearCards();
            
            yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);

            endingsPanel.SetActive(true);
            
            endingTitle.text = "Noia Ending";
            endingDescription.text = "Noia got the best of you. You will never make a good decision again";
            endingScore.text = "Score: " + score;
        }
        
        private IEnumerator SurvivalEndingRoutine()
        {
            DialogueManager.Instance.AddDialogue(survivalEndingDialogueLines);
            hasEnded = true;
            DecisionCardManager.Instance.ClearCards();
            
            yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);

            endingsPanel.SetActive(true);
            
            endingTitle.text = "Survival Ending";
            endingDescription.text = "You made some tough decisions and have proven capable of making your own choices from here";
            endingScore.text = "Score: " + score;
        }
        
        private IEnumerator BestEndingRoutine()
        {
            DialogueManager.Instance.AddDialogue(bestEndingDialogueLines);
            hasEnded = true;
            DecisionCardManager.Instance.ClearCards();
            
            yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);

            endingsPanel.SetActive(true);
            
            endingTitle.text = "Golden Ending";
            endingDescription.text = "You proved to be an incredible leader. Your village will honor you forever";
            endingScore.text = "Score: " + score;
        }

        private float calculateScore()
        {
            // Get actual values later
            const float maxResource = 9999f;
            const float maxEmotion = 1000f;
            const float maxPopulation = 1000f;
            
            float foodCount = ResourceManager.Instance.GetAmount(ResourceType.Food);
            float woodCount = ResourceManager.Instance.GetAmount(ResourceType.Wood);
            float goldCount = ResourceManager.Instance.GetAmount(ResourceType.Gold);
            float emotionCount = ResourceManager.Instance.GetAmount(ResourceType.Emotion);
            float populationCount = VillagerManager.Instance.population;
            float cluesCount = ResourceManager.Instance.GetAmount(ResourceType.Clues);
            
            float foodScore = foodCount / maxResource;
            float woodScore = woodCount / maxResource;
            float goldScore = goldCount / maxResource;
            float emotionScore = emotionCount / maxEmotion;
            float populationScore = populationCount / maxPopulation;
            
            float cluePenalty = Mathf.Max(0, cluesCount - 1);
            
            float score =
                (foodScore * 3000f) +
                (goldScore * 3000f) +
                (emotionScore * 5000f) +
                (populationScore * 5000f) +
                (woodScore * 1000f) -
                (cluePenalty * 250f);

            Debug.Log($"Final Score: {score}");
            return score;
        }
        
        
    }
}