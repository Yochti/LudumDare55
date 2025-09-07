using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class HealthButton : MonoBehaviour
{
    public saveSytem save;

    public Image[] healthImages = new Image[3];
    public Image[] damageImages = new Image[3];
    public Image[] attackSpeedImages = new Image[3];
    public Image[] speedImages = new Image[3];
    //public Image[] healthRegenImages = new Image[3];
    public Image[] critChanceImages = new Image[3];
    public Image[] critDamageImages = new Image[3];
    public Image[] luck = new Image[3];


    private void Update()
    {
        UpdateUpgradeVisuals(save.HealthUpgrades, healthImages);
        UpdateUpgradeVisuals(save.DamagesUpgrades, damageImages);
        UpdateUpgradeVisuals(save.AttackSpeedUpgrades, attackSpeedImages);
        UpdateUpgradeVisuals(save.SpeedUpgrades, speedImages);
        //UpdateUpgradeVisuals(save.HealthRegen, healthRegenImages);
        UpdateUpgradeVisuals(save.CritChance, critChanceImages);
        UpdateUpgradeVisuals(save.CritDamage, critDamageImages);
        UpdateUpgradeVisuals(save.Luck, luck);
    }

    private void UpdateUpgradeVisuals(int upgradeLevel, Image[] images)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < upgradeLevel)
            {
                Color newColor;
                if (ColorUtility.TryParseHtmlString("#FFFFFF", out newColor))
                {
                    images[i].color = newColor;
                }
            }
        }
    }



}
