using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        [Header("UI")]
        public GameObject dialoguePanel;
        public TMP_Text dialogueText;

        [Header("Typewriter")]
        [TextArea]
        public string dialogue;
        private bool isTyping;

        public float typingSpeed = 0.04f;

        private Coroutine typingCoroutine;

        void Start()
        {
            dialoguePanel.SetActive(false);
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTyping)
                {
                    StopCoroutine(typingCoroutine);
                    dialogueText.text = dialogue;
                    isTyping = false;
                }
            }
        }

        public void ShowDialogue(string text)
        {
            DialogueManager.Instance.isDiaglogueActive = true;
            dialoguePanel.SetActive(true);
            
            dialogue = text;

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeText());
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

            foreach (char letter in dialogue)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            isTyping = false;
        }
    }
}