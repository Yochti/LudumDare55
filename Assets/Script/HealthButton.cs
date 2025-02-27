using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class HealthButton : MonoBehaviour, IPointerEnterHandler
{
    // UI References
    public Image imageRight;
    public Sprite healthSprite;

    public LocalizedString localizedName;
    public LocalizedString localizedDescription;

    public TextMeshProUGUI HealthName; // Pour le nom
    public TextMeshProUGUI HealthDescription; // Pour la description

    public saveSytem save;

    private string translatedName;
    private string translatedDescription;

    // References
    public BuyUpgrades buyUpgrades;

    public Image[] healthImages = new Image[3];
    public Image[] damageImages = new Image[3];
    public Image[] attackSpeedImages = new Image[3];
    public Image[] speedImages = new Image[3];
    public Image[] healthRegenImages = new Image[3];
    public Image[] invincibilityFrameImages = new Image[3];
    public Image[] critChanceImages = new Image[3];
    public Image[] critDamageImages = new Image[3];
    public Image[] soulsAttractImages = new Image[3];

    private void Start()
    {
        localizedName.StringChanged += (translatedText) => { translatedName = translatedText; };
        localizedDescription.StringChanged += (translatedText) => { translatedDescription = translatedText; };

        localizedName.RefreshString();
        localizedDescription.RefreshString();
    }

    private void Update()
    {
        UpdateUpgradeVisuals(save.HealthUpgrades, healthImages);
        UpdateUpgradeVisuals(save.DamagesUpgrades, damageImages);
        UpdateUpgradeVisuals(save.AttackSpeedUpgrades, attackSpeedImages);
        UpdateUpgradeVisuals(save.SpeedUpgrades, speedImages);
        UpdateUpgradeVisuals(save.HealthRegen, healthRegenImages);
        UpdateUpgradeVisuals(save.InvincibilityFrame, invincibilityFrameImages);
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
        HealthName.text = translatedName;
        HealthDescription.text = translatedDescription;
    }

    private void OnDestroy()
    {
        localizedName.StringChanged -= (translatedText) => { translatedName = translatedText; };
        localizedDescription.StringChanged -= (translatedText) => { translatedDescription = translatedText; };
    }
}
