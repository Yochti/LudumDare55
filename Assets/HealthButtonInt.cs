using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class HealthButtonInt : MonoBehaviour, IPointerEnterHandler
{
    public saveSytem save;
    public TextMeshProUGUI textActual;
    public TextMeshProUGUI textNext;
    public TextMeshProUGUI textPrice;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(save.HealthUpgrades < 1)
        {
            textActual.text = "200";
            textNext.text = "300";
            textPrice.text = "1250";
        }
        else if(save.HealthUpgrades < 2)
        {
            textActual.text = "300";
            textNext.text = "400";
            textPrice.text = "5000";
        }
        else if(save.HealthUpgrades < 3)
        {
            textActual.text = "400";
            textNext.text = "500";
            textPrice.text = "10000";
        }
        else
        {
            textActual.text = "400";
            textNext.text = "Max level";
            textPrice.text = "N/A";
        }
    }
}
