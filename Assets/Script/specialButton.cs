using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SpecialButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SpecialBuyAndPlay specialBuyAndPlay;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        specialBuyAndPlay.OnPointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        specialBuyAndPlay.OnPointerUp();
    }
}
