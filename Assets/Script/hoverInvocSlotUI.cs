using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hoverInvocSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject invocPanel;
    private Vector3 originalScale;
    public float scaleFactor = 1.2f;
    public float bounceDuration = 0.3f;
    public AudioSource audioS;

    private Coroutine scaleCoroutine;

    private void Start()
    {
        originalScale = invocPanel.transform.localScale;
    }
    private void OnEnable()
    {
        //invocPanel.transform.localScale = originalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioS.Play();

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ScaleBounceEffect());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }

        invocPanel.transform.localScale = originalScale;
    }

    private IEnumerator ScaleBounceEffect()
    {
        Vector3 targetScale = originalScale * scaleFactor;
        Vector3 smallScale = originalScale * (scaleFactor - 0.05f);
        float t;

        // Phase 1: Agrandir rapidement
        t = 0f;
        while (t < 1f)
        {
            invocPanel.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            t += Time.unscaledDeltaTime / (bounceDuration / 4f);
            yield return null;
        }

        // Phase 2: Rétrécir un peu
        t = 0f;
        while (t < 1f)
        {
            invocPanel.transform.localScale = Vector3.Lerp(targetScale, smallScale, t);
            t += Time.unscaledDeltaTime / (bounceDuration / 3f);
            yield return null;
        }

        // Phase 3: Revenir à l'échelle finale
        t = 0f;
        while (t < 1f)
        {
            invocPanel.transform.localScale = Vector3.Lerp(smallScale, targetScale, t);
            t += Time.unscaledDeltaTime / (bounceDuration / 4f);
            yield return null;
        }

        invocPanel.transform.localScale = targetScale;

        scaleCoroutine = null;
    }
}
