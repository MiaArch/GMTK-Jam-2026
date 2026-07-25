using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Decisions
{
    public class EffectRow : MonoBehaviour
    {
        public Image icon;
        public TMP_Text text;

        public void Setup(Sprite sprite, string description, bool positive)
        {
            icon.sprite = sprite;
            text.text = description;
            text.color = positive ? new Color(0, 0.5f, 0) : new Color(0.5f, 0, 0);
        }
    }
}