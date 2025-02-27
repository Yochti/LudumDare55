using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hoverInvocSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject invocPanel;
    private Vector3 originalScale;
    public float scaleFactor = 1.2f; // Taille maximale
    public float bounceDuration = 0.3f; // Durée du rebond
    public AudioSource audioS;

    private void Start()
    {
        originalScale = invocPanel.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioS.Play();
        StartCoroutine(ScaleBounceEffect());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(ScaleBounceEffect());
        invocPanel.transform.localScale = originalScale;  // Retour à la taille d'origine
    }

    private IEnumerator ScaleBounceEffect()
    {
        Vector3 targetScale = originalScale * scaleFactor;
        Vector3 currentScale = originalScale;

        float timeElapsed = 0f;

        // Phase 1: Agrandir rapidement
        while (timeElapsed < bounceDuration / 4)
        {
            currentScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / (bounceDuration / 4));
            invocPanel.transform.localScale = currentScale;
            timeElapsed += Time.unscaledDeltaTime; // Utilisation de Time.unscaledDeltaTime
            yield return null;
        }

        // Phase 2: Rétrécir légèrement (effet de rebond)
        timeElapsed = 0f;
        Vector3 smallScale = originalScale * (scaleFactor - 0.05f);  // Un peu plus petit que la taille cible
        while (timeElapsed < bounceDuration / 3)
        {
            currentScale = Vector3.Lerp(targetScale, smallScale, timeElapsed / (bounceDuration / 3));
            invocPanel.transform.localScale = currentScale;
            timeElapsed += Time.unscaledDeltaTime; // Utilisation de Time.unscaledDeltaTime
            yield return null;
        }

        // Phase 3: Reprendre et arriver à la taille finale
        timeElapsed = 0f;
        while (timeElapsed < bounceDuration / 4)
        {
            currentScale = Vector3.Lerp(smallScale, targetScale, timeElapsed / (bounceDuration / 4));
            invocPanel.transform.localScale = currentScale;
            timeElapsed += Time.unscaledDeltaTime; // Utilisation de Time.unscaledDeltaTime
            yield return null;
        }

        // Assurer que l'échelle finale est atteinte
        invocPanel.transform.localScale = targetScale;
    }
}
