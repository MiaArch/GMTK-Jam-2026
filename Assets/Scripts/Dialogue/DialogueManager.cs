using System;
using UnityEngine;
using Utils;

namespace Dialogue
{
    public class DialogueManager: Singleton<DialogueManager>
    {
        public DialogueUI dialogueUI;
        public bool isDiaglogueActive;

        void Start()
        {
            isDiaglogueActive = false;
            
        }

        public void AddDialogue(String dialogue)
        {
            dialogueUI.ShowDialogue(dialogue
            );
        }
    }
}