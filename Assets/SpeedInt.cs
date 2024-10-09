using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class SpeedInt : MonoBehaviour, IPointerEnterHandler
{
    public saveSytem save;
    public TextMeshProUGUI textActual;
    public TextMeshProUGUI textNext;
    public TextMeshProUGUI textPrice;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (save.SpeedUpgrades < 1)
        {
            textActual.text = "5";
            textNext.text = "5.5";
            textPrice.text = "1250";
        }
        else if (save.SpeedUpgrades < 2)
        {
            textActual.text = "5.5";
            textNext.text = "6";
            textPrice.text = "5000";
        }
        else if (save.SpeedUpgrades < 3)
        {
            textActual.text = "6";
            textNext.text = "7";
            textPrice.text = "10000";
        }
        else
        {
            textActual.text = "7";
            textNext.text = "Max level";
            textPrice.text = "N/A";
        }
    }
}
