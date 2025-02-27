using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class DamageButtonInt : MonoBehaviour, IPointerEnterHandler
{
    public saveSytem save;
    public TextMeshProUGUI textActual;
    public TextMeshProUGUI textNext;
    public TextMeshProUGUI textPrice;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (save.DamagesUpgrades < 1)
        {
            textActual.text = "+ 0%";
            textNext.text = "+ 10%";
            textPrice.text = "1250";
        }
        else if (save.DamagesUpgrades < 2)
        {
            textActual.text = "+ 10%";
            textNext.text = "+ 20% ";
            textPrice.text = "2500";
        }
        else if (save.DamagesUpgrades < 3)
        {
            textActual.text = "+ 20%";
            textNext.text = "+ 30%";
            textPrice.text = "5000";
        }
        else
        {
            textActual.text = "+ 30%";
            textNext.text = "Max level";
            textPrice.text = "N/A";
        }
    }
}
