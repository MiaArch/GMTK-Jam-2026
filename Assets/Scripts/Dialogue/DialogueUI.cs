using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        [Header("UI")]
        public GameObject dialoguePanel;
        public TMP_Text dialogueText;
        public GameObject continueButton;

        [Header("Typewriter")]
        [TextArea]
        public List<String> dialogueLines;
        private int currentLine = 0;
        private bool isTyping;

        public float typingSpeed = 0.04f;

        private Coroutine typingCoroutine;

        void Start()
        {
            dialoguePanel.SetActive(false);
            continueButton.SetActive(false);
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTyping)
                {
                    StopCoroutine(typingCoroutine);
                    dialogueText.text = dialogueLines[currentLine];
                    isTyping = false;
                    continueButton.SetActive(true);
                }
                else
                {
                    ContinueDialogue();
                }
            }
        }

        public void ShowDialogue(List<string> text)
        {
            DialogueManager.Instance.isDiaglogueActive = true;
            dialoguePanel.SetActive(true);
            
            dialogueLines = text;

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeText());
        }

        public void ContinueDialogue()
        {
            currentLine++;
            if (currentLine >= dialogueLines.Count)
            {
                HideDialogue();
            }
            else
            {
                typingCoroutine = StartCoroutine(TypeText());
            }
        }

        public void HideDialogue()
        {
            DialogueManager.Instance.isDiaglogueActive = false;
            dialoguePanel.SetActive(false);

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);
        }

        IEnumerator TypeText()
        {
            isTyping = true;
            dialogueText.text = "";

            foreach (char letter in dialogueLines[currentLine])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            continueButton.SetActive(true);
            isTyping = false;
        }
    }
}