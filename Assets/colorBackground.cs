using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorBackground : MonoBehaviour
{
    [Header("Composants")]
    public Image backgroundImage;

    [Header("Couleurs par rareté")]
    public Color commonColor = Color.gray;
    public Color rareColor = Color.blue;
    public Color epicColor = new Color(0.6f, 0f, 1f); // violet
    public Color legendaryColor = Color.yellow;

    private PowerUpWaves.Rarity highestRarity;
    private Color lastHoveredColor;
    private bool hoveredOnce = false;

    private void Start()
    {
        if (backgroundImage == null)
            backgroundImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        hoveredOnce = false;
        ApplyHighestRarityColor();
    }

    public void SetColors(List<PowerUpWaves.Rarity> rarities)
    {
        highestRarity = GetHighestRarity(rarities);

        if (!hoveredOnce && gameObject.activeInHierarchy)
        {
            ApplyHighestRarityColor();
        }
    }

    public void OnButtonHover(PowerUpWaves.Rarity rarity)
    {
        Color hoverColor = GetColorFromRarity(rarity);
        backgroundImage.color = hoverColor;
        lastHoveredColor = hoverColor;
        hoveredOnce = true;
    }

    public void OnButtonExit()
    {
        if (hoveredOnce)
        {
            backgroundImage.color = lastHoveredColor;
        }
        else
        {
            ApplyHighestRarityColor();
        }
    }

    private void ApplyHighestRarityColor()
    {
        Color highestColor = GetColorFromRarity(highestRarity);
        backgroundImage.color = highestColor;
        lastHoveredColor = highestColor; // Initialiser aussi ici
    }

    private PowerUpWaves.Rarity GetHighestRarity(List<PowerUpWaves.Rarity> rarities)
    {
        PowerUpWaves.Rarity highest = PowerUpWaves.Rarity.Common;
        foreach (var rarity in rarities)
        {
            if ((int)rarity > (int)highest)
                highest = rarity;
        }
        return highest;
    }

    private Color GetColorFromRarity(PowerUpWaves.Rarity rarity)
    {
        switch (rarity)
        {
            case PowerUpWaves.Rarity.Common: return commonColor;
            case PowerUpWaves.Rarity.Rare: return rareColor;
            case PowerUpWaves.Rarity.Epic: return epicColor;
            case PowerUpWaves.Rarity.Legendary: return legendaryColor;
            default: return Color.white;
        }

    }
}
