using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Dialogue
{
    public class DialogueManager: Singleton<DialogueManager>
    {
        public DialogueUI dialogueUI;
        public bool isDialogueActive;

        void Start()
        {
            isDialogueActive = false;
            
        }

        public void AddDialogue(List<String> dialogue)
        {
            dialogueUI.ShowDialogue(dialogue
            );
        }

        public void AddDialogue(String dialogue)
        {
            List<String> dialogueList = new List<String>();
            dialogueList.Add(dialogue);
            dialogueUI.ShowDialogue(dialogueList);
        }
    }
}