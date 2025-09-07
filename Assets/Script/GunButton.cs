using UnityEngine;
using UnityEngine.EventSystems;

public class GunButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GunBuyAndPlay gunBuyAndPlay;
    public int gunIndex;
    public string gunName;
    public int price;

    public void OnPointerDown(PointerEventData eventData)
    {
        //gunBuyAndPlay.OnPointerDown(gunIndex, gunName, price);
        gunBuyAndPlay.OnPointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gunBuyAndPlay.OnPointerUp();
    }
}
