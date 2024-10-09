using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class FrameInt : MonoBehaviour, IPointerEnterHandler
{
    public saveSytem save;
    public TextMeshProUGUI textActual;
    public TextMeshProUGUI textNext;
    public TextMeshProUGUI textPrice;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (save.InvincibilityFrame < 1)
        {
            textActual.text = "0.3";
            textNext.text = "0.5";
            textPrice.text = "1250";
        }
        else if (save.InvincibilityFrame < 2)
        {
            textActual.text = "0.5";
            textNext.text = "0.7";
            textPrice.text = "5000";
        }
        else if (save.InvincibilityFrame < 3)
        {
            textActual.text = "0.7";
            textNext.text = "1";
            textPrice.text = "10000";
        }
        else
        {
            textActual.text = "1";
            textNext.text = "Max level";
            textPrice.text = "N/A";
        }
    }
}
