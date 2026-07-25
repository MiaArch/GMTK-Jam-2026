using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

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
        private readonly Queue<List<string>> dialogueQueue = new();
        private int currentLine = 0;
        private bool isTyping;
        [SerializeField] private AudioClip villagerNoise;

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
            if (text == null || text.Count == 0)
                return;

            // If a dialogue is already playing, queue this one.
            if (DialogueManager.Instance.isDialogueActive)
            {
                dialogueQueue.Enqueue(new List<string>(text));
                return;
            }

            StartDialogue(text);
        }

        private void StartDialogue(List<string> text)
        {
            DialogueManager.Instance.isDialogueActive = true;
            dialoguePanel.SetActive(true);

            dialogueLines = text;
            currentLine = 0;

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
            DialogueManager.Instance.isDialogueActive = false;
            dialoguePanel.SetActive(false);

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);
            if (dialogueQueue.Count > 0)
            {
                StartDialogue(dialogueQueue.Dequeue());
                return;
            }

            typingCoroutine = null;
            currentLine = 0;
            isTyping = false;
            AudioManager.Instance.StopSFX();
            
        }

        IEnumerator TypeText()
        {
            isTyping = true;
            continueButton.SetActive(false);
            dialogueText.text = "";
            AudioManager.Instance.LoopSFX();
            AudioManager.Instance.PlaySFXWithPitchShifting(villagerNoise, 1.05f, 0.95f);
            

            foreach (char letter in dialogueLines[currentLine])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            continueButton.SetActive(true);
            isTyping = false;
            AudioManager.Instance.StopSFX();
        }
    }
}