using UnityEngine;
using UnityEngine.EventSystems;

public class PowerUpButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PowerUpWaves.Rarity rarity;
    public colorBackground backgroundController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        backgroundController.OnButtonHover(rarity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backgroundController.OnButtonExit();
    }
}
