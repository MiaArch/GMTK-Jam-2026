using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ConfirmationDialog : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] TMP_Text title;
        [SerializeField] TMP_Text message;

        Action yesAction;
        Action noAction;

        public void Show(
            string titleText,
            string messageText,
            Action onYes,
            Action onNo = null)
        {
            panel.SetActive(true);

            title.text = titleText;
            message.text = messageText;

            yesAction = onYes;
            noAction = onNo;
        }

        public void Yes()
        {
            panel.SetActive(false);
            yesAction?.Invoke();
        }

        public void No()
        {
            panel.SetActive(false);
            noAction?.Invoke();
        }
    }
}