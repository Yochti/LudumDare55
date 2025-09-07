using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class InvocButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InvocShopBuy invocShopBuy;


    public void OnPointerDown(PointerEventData eventData)
    {
        invocShopBuy.OnPointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        invocShopBuy.OnPointerUp();
    }
}
