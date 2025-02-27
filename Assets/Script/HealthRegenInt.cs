using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class HealthRegenInt : MonoBehaviour, IPointerEnterHandler
{
    public saveSytem save;
    public TextMeshProUGUI textActual;
    public TextMeshProUGUI textNext;
    public TextMeshProUGUI textPrice;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (save.HealthRegen < 1)
        {
            textActual.text = "0h/sec";
            textNext.text = "1h/sec";
            textPrice.text = "1250";
        }
        else if (save.HealthRegen < 2)
        {
            textActual.text = "1h/sec";
            textNext.text = "2h/sec";
            textPrice.text = "2500";
        }
        else if (save.HealthRegen < 3)
        {
            textActual.text = "2h/sec";
            textNext.text = "3h/sec";
            textPrice.text = "5000";
        }
        else
        {
            textActual.text = "3h/sec";
            textNext.text = "Max level";
            textPrice.text = "N/A";
        }
    }
}
