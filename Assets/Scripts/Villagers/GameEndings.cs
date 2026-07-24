using System;
using System.Collections;
using System.Collections.Generic;
using Decisions;
using Dialogue;
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


        [Header("Required Ending Values")]
        [SerializeField] private int paranoiaCluesRequired;
        [SerializeField] private int populationRequirementBestEnding;
        [SerializeField] private int emotionRequirementBestEnding;

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

        public void TriggerNoiaEnding()
        {
            //TODO:
        }

        public void TriggerSurvivalEnding()
        {
            //TODO:
        }

        public void TriggerBestEnding()
        {
            //TODO:
        }

        private IEnumerator BadEndingRoutine()
        {
            DialogueManager.Instance.AddDialogue(badEndingDialogueLines);
            hasEnded = true;

            yield return new WaitUntil(() => !DialogueManager.Instance.isDialogueActive);

            endingsPanel.SetActive(true);
            
            endingTitle.text = "Lonely Ending";
            endingDescription.text = "Through your failures, you doomed your village.";
            
            DecisionCardManager.Instance.ClearCards();
        }
        
    }
}