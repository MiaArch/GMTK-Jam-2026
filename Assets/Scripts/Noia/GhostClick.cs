using System;
using Resource;
using UnityEngine;
using UnityEngine.UI;

namespace Noia
{
    public class GhostClick: MonoBehaviour
    {
        public Image ghostImage;
        private Button ghostButton;
        public bool clickedBefore;

        void Awake()
        {
            ghostImage = GetComponent<Image>();
            ghostButton = GetComponent<Button>();
            ghostButton.interactable = false;
            ghostImage.raycastTarget = false;
        }

        private void Update()
        {
            if (ghostImage.color.a > 0.01f && !clickedBefore)
            {
                ghostImage.raycastTarget = true;
                ghostButton.interactable = true;
            }
        }

        public void OnClick()
        {
            Debug.Log("Clicked Ghost");
            ghostImage.raycastTarget = false;
            ghostButton.interactable = false;
            clickedBefore = true;
            ResourceManager.Instance.AddClue(1);
            ResourceManager.Instance.AddGold(5);
        }
    }
}