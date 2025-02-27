using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowZone : MonoBehaviour
{
    public float growthDuration = 0.2f;
    public float growthFactor = 1.5f;
    public static float damageMultiplier = 1.5f;

    private Transform playerTransform;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private float growthElapsedTime = 0f;
    private bool isInZone = false;
    private bool isGrowing = false;
    private bool isShrinking = false;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        originalScale = playerTransform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isGrowing)
        {
            isInZone = true;
            StartGrowing();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isInZone)
        {
            isInZone = false;
            StartShrinking();
        }
    }

    private void StartGrowing()
    {
        isGrowing = true;
        targetScale = originalScale * growthFactor;
        growthElapsedTime = 0f; // Reset le temps écoulé
    }

    private void StartShrinking()
    {
        isShrinking = true;
        targetScale = originalScale;
        growthElapsedTime = 0f; // Reset le temps écoulé
    }

    private void Update()
    {
        if (isGrowing)
        {
            growthElapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(growthElapsedTime / growthDuration);
            playerTransform.localScale = Vector3.Lerp(originalScale, targetScale, progress);

            if (progress >= 1f)
            {
                PlayerStats.additionalDamage *= damageMultiplier;
                isGrowing = false;
            }
        }

        if (isShrinking)
        {
            growthElapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(growthElapsedTime / growthDuration);
            playerTransform.localScale = Vector3.Lerp(playerTransform.localScale, targetScale, progress);

            if (progress >= 1f)
            {
                PlayerStats.additionalDamage /= damageMultiplier;
                isShrinking = false;
            }
        }
    }
}
