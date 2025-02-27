using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisibleZone : MonoBehaviour
{
    public float invisibleAlpha = 0.15f;
    public float fadeDuration = 0.5f;
    public static bool isInvisible;
    public static Vector3 lastKnownPosition;

    private SpriteRenderer playerSprite;
    private Transform playerTransform;
    private float originalAlpha;
    private bool isInZone = false;

    private float fadeElapsedTime = 0f;
    private bool isFadingToInvisible = false;
    private bool isFadingToVisible = false;

    private void Start()
    {
        playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        playerTransform = playerSprite.transform;

        originalAlpha = playerSprite.color.a;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInZone)
        {
            isInvisible = true;
            isInZone = true;

            lastKnownPosition = playerTransform.position;
            StartFadingToInvisible();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isInZone)
        {
            isInvisible = false;
            isInZone = false;
            StartFadingToVisible();
        }
    }

    private void StartFadingToInvisible()
    {
        fadeElapsedTime = 0f; // Réinitialise le temps écoulé
        isFadingToInvisible = true; // Indique que l'on fade vers l'invisible
    }

    private void StartFadingToVisible()
    {
        fadeElapsedTime = 0f; // Réinitialise le temps écoulé
        isFadingToVisible = true; // Indique que l'on fade vers la visibilité
    }

    private void Update()
    {
        if (isFadingToInvisible)
        {
            fadeElapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(fadeElapsedTime / fadeDuration);
            Color currentColor = playerSprite.color;
            currentColor.a = Mathf.Lerp(originalAlpha, invisibleAlpha, progress);
            playerSprite.color = currentColor;

            if (progress >= 1f)
            {
                isFadingToInvisible = false; // Fin de l'opération de fondu
            }
        }

        if (isFadingToVisible)
        {
            fadeElapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(fadeElapsedTime / fadeDuration);
            Color currentColor = playerSprite.color;
            currentColor.a = Mathf.Lerp(playerSprite.color.a, originalAlpha, progress);
            playerSprite.color = currentColor;

            if (progress >= 1f)
            {
                isFadingToVisible = false; // Fin de l'opération de fondu
            }
        }
    }
}
