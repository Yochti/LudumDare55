using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HealthButton : MonoBehaviour, IPointerEnterHandler
{
    // UI References
    public Image imageRight;
    public Sprite healthSprite;
    public TextMeshProUGUI HealthName;
    public string Name;
    public string Description;
    public TextMeshProUGUI HealthDescription;
    public saveSytem save;

    // References
    public BuyUpgrades buyUpgrades;

    // Ameliorations Images (3 levels per amelioration)
    public Image[] healthImages = new Image[3];
    public Image[] damageImages = new Image[3];
    public Image[] attackSpeedImages = new Image[3];
    public Image[] speedImages = new Image[3];
    public Image[] healthRegenImages = new Image[3];
    public Image[] invincibilityFrameImages = new Image[3];
    public Image[] critChanceImages = new Image[3];
    public Image[] critDamageImages = new Image[3];
    public Image[] soulsAttractImages = new Image[3];

    private void Update()
    {
        UpdateUpgradeVisuals(save.HealthUpgrades, healthImages);
        UpdateUpgradeVisuals(save.DamagesUpgrades, damageImages);
        UpdateUpgradeVisuals(save.AttackSpeedUpgrades, attackSpeedImages);
        UpdateUpgradeVisuals(save.SpeedUpgrades, speedImages);
        UpdateUpgradeVisuals(save.HealthRegen, healthRegenImages);
        UpdateUpgradeVisuals(save.InvincibilityFrame,  invincibilityFrameImages);
        UpdateUpgradeVisuals(save.CritChance, critChanceImages);
        UpdateUpgradeVisuals(save.CritDamage, critDamageImages);
        UpdateUpgradeVisuals(save.SoulsAttract, soulsAttractImages);
    }

    private void UpdateUpgradeVisuals(int upgradeLevel, Image[] images)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < upgradeLevel)
            {
                Color newColor;
                if (ColorUtility.TryParseHtmlString("#BFBFBF", out newColor))
                {
                    images[i].color = newColor;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageRight.sprite = healthSprite;
        HealthName.text = Description;
        HealthDescription.text = Name;
        
    }
}
