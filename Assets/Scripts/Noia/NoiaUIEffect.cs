using System.Collections;
using System.Collections.Generic;
using Resource;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Noia
{
    [RequireComponent(typeof(Image))]
    public class NoiaUIEffect : MonoBehaviour
{
        [Header("General")]
        [SerializeField] private int cluesForMaxIntensity = 10;

        [Header("Vignette")]
        [SerializeField, Range(0, 1)] private float maxAlpha = 0.8f;

        [Header("UI Drift")]
        [SerializeField] private RectTransform[] driftingUI;
        [SerializeField] private float maxDrift = 8f;
        [SerializeField] private float driftSpeed = 0.5f;

        [Header("Ghosts")]
        [SerializeField] private List<GhostClick> ghostImages;

        [SerializeField] private AudioClip breath;
        [SerializeField] private float ghostAlpha = 0.3f;
        [SerializeField] private float ghostFadeTime = 0.08f;
        [SerializeField] private float ghostVisibleTime = 0.12f;
        [SerializeField] private float ghostCooldown = 5f;
        private bool ghostActive;

        private Image vignette;
        private Vector2[] originalPositions;

        private void Awake()
        {
            vignette = GetComponent<Image>();

            originalPositions = new Vector2[driftingUI.Length];
            for (int i = 0; i < driftingUI.Length; i++)
                originalPositions[i] = driftingUI[i].anchoredPosition;
        }

        private void Update()
        {
            int clues = ResourceManager.Instance.GetAmount(ResourceType.Clues);

            float intensity = Mathf.Clamp01((float)clues / cluesForMaxIntensity);

            UpdateVignette(intensity);
            UpdateDrift(intensity);
            
            if (clues >= 3 && !ghostActive && ghostImages.Count != 0)
            {
                ghostActive = true;
                StartCoroutine(FlashGhost());
            }
        }

        private void UpdateVignette(float intensity)
        {
            Color c = vignette.color;
            c.a = intensity * maxAlpha;
            vignette.color = c;
        }

        private void UpdateDrift(float intensity)
        {
            for (int i = 0; i < driftingUI.Length; i++)
            {
                Vector2 offset = new Vector2(
                    Mathf.Sin(Time.time * driftSpeed + i),
                    Mathf.Cos(Time.time * driftSpeed * 1.2f + i));

                driftingUI[i].anchoredPosition =
                    originalPositions[i] + offset * (maxDrift * intensity);
            }
        }
        
        private IEnumerator FlashGhost()
        {
            if (ghostImages.Count == 0)
                yield break;

            GhostClick ghostClick = ghostImages[Random.Range(0, ghostImages.Count)];
            if (ghostClick.clickedBefore)
            {
                ghostImages.Remove(ghostClick);
                yield return new WaitForSeconds(ghostCooldown);
                ghostActive = false;
                yield break;
            }
            AudioManager.Instance.PlaySFXWithPitchShifting(breath, 0.95f, 1.05f);
            Image ghost = ghostClick.ghostImage;
            Color ghostColour = ghost.color;
            ghostColour.a = 0f;
            ghost.color = ghostColour;
            ghost.gameObject.SetActive(true);
            
            float ghostTime = 0f;
            while (ghostTime < ghostFadeTime)
            {
                ghostTime += Time.deltaTime;
                ghostColour.a = Mathf.Lerp(0f, ghostAlpha, ghostTime / ghostFadeTime);
                ghost.color = ghostColour;
                yield return null;
            }

            yield return new WaitForSeconds(ghostVisibleTime);
            
            ghostTime = 0f;
            while (ghostTime < ghostFadeTime)
            {
                ghostTime += Time.deltaTime;
                ghostColour.a = Mathf.Lerp(ghostAlpha, 0f, ghostTime / ghostFadeTime);
                ghost.color = ghostColour;
                yield return null;
            }

            ghostColour.a = 0f;
            ghost.color = ghostColour;
            ghost.gameObject.SetActive(false);

            yield return new WaitForSeconds(ghostCooldown);
            ghostActive = false;
        }
    }
}