using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hoverInvocSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject invocPanel;
    private Vector3 originalScale;
    public float scaleFactor = 1.2f;
    public AudioSource audioS;

    private void Start()
    {
        originalScale = invocPanel.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        invocPanel.transform.localScale = originalScale * scaleFactor;
        audioS.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        invocPanel.transform.localScale = originalScale;
    }
}
