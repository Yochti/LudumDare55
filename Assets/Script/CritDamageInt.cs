using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class CritDamageInt : MonoBehaviour, IPointerEnterHandler
{
    public saveSytem save;
    public TextMeshProUGUI textActual;
    public TextMeshProUGUI textNext;
    public TextMeshProUGUI textPrice;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (save.CritDamage < 1)
        {
            textActual.text = "200%";
            textNext.text = "220%";
            textPrice.text = "1250";
        }
        else if (save.CritDamage < 2)
        {
            textActual.text = "220%";
            textNext.text = "250%";
            textPrice.text = "2500";
        }
        else if (save.CritDamage < 3)
        {
            textActual.text = "250%";
            textNext.text = "300%";
            textPrice.text = "5000";
        }
        else
        {
            textActual.text = "300%";
            textNext.text = "Max level";
            textPrice.text = "N/A";
        }
    }
}
