using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InvocSlotManager : MonoBehaviour
{
    public saveSytem save;

    [Header("Slots UI")]
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;
    public Color emptySlotColor;

    [Header("Icons par invocation")]
    public Sprite iconBababoy;
    public Sprite iconName;
    public Sprite iconJunktrap;
    public Sprite iconHermes;
    public Sprite iconHera;
    public Sprite iconIzo;

    private Dictionary<string, Sprite> invocIcons;

    private void Start()
    {
        invocIcons = new Dictionary<string, Sprite>
        {
            { "Bababoy", iconBababoy },
            { "Name", iconName },
            { "Junktrap", iconJunktrap },
            { "Hermes", iconHermes },
            { "Hera", iconHera },
            {"Izo", iconIzo }
        };

        UpdateSlotImages();
    }

    private void Update()
    {
        UpdateSlotImages(); 
    }

    public void UpdateSlotImages()
    {
        UpdateSlotImage(slot1, save.InvocSlot1);
        UpdateSlotImage(slot2, save.InvocSlot2);
        UpdateSlotImage(slot3, save.InvocSlot3);
        UpdateSlotImage(slot4, save.InvocSlot4);
    }

    private void UpdateSlotImage(Image slotImage, string invocName)
    {
        if (!string.IsNullOrEmpty(invocName) && invocIcons.ContainsKey(invocName))
        {
            slotImage.sprite = invocIcons[invocName];
            slotImage.color = Color.white;
        }
        else
        {
            slotImage.sprite = null;
            slotImage.color = emptySlotColor;
        }
    }
}
