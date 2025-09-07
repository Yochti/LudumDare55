using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyUpgrades : MonoBehaviour
{
    // References
    public saveSytem save;

    // Prices
    public int price1 = 1250;
    public int price2 = 5000;
    public int price3 = 10000;

    // Audio
    public AudioSource audioo;  // Achat réussi
    public AudioSource audioo2; // Achat refusé

    // Price Texts
    public TextMeshProUGUI healthPriceText;
    public TextMeshProUGUI damagePriceText;
    public TextMeshProUGUI atkSpeedPriceText;
    public TextMeshProUGUI speedPriceText;
    //public TextMeshProUGUI regenPriceText;
    public TextMeshProUGUI critChancePriceText;
    public TextMeshProUGUI critDamagePriceText;
    public TextMeshProUGUI luckPriceText;

    private void Start()
    {
        UpdateAllPrices();
    }

    private void UpdatePriceText(TextMeshProUGUI text, int level)
    {
        switch (level)
        {
            case 0:
                text.text = price1.ToString();
                break;
            case 1:
                text.text = price2.ToString();
                break;
            case 2:
                text.text = price3.ToString();
                break;
            default:
                text.text = "MAX";
                break;
        }
    }

    private void UpdateAllPrices()
    {
        UpdatePriceText(healthPriceText, save.HealthUpgrades);
        UpdatePriceText(damagePriceText, save.DamagesUpgrades);
        UpdatePriceText(atkSpeedPriceText, save.AttackSpeedUpgrades);
        UpdatePriceText(speedPriceText, save.SpeedUpgrades);
        //UpdatePriceText(regenPriceText, save.HealthRegen);
        UpdatePriceText(critChancePriceText, save.CritChance);
        UpdatePriceText(critDamagePriceText, save.CritDamage);
        UpdatePriceText(luckPriceText, save.Luck);
    }

    public void BuyHealthUpgrades()
    {
        if (save.HealthUpgrades < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.HealthUpgrades++;
            save.PlayerHealth = 200;
            save.SaveData();
        }
        else if (save.HealthUpgrades < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.HealthUpgrades++;
            save.PlayerHealth = 250;
            save.SaveData();
        }
        else if (save.HealthUpgrades < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.HealthUpgrades++;
            save.PlayerHealth = 300;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }

        UpdatePriceText(healthPriceText, save.HealthUpgrades);
    }

    public void BuyDamageUpgrades()
    {
        if (save.DamagesUpgrades < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.DamagesUpgrades++;
            save.PlayerDamages = 0.05f;
            save.SaveData();
        }
        else if (save.DamagesUpgrades < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerDamages = 0.1f;
            save.DamagesUpgrades++;
            save.SaveData();
        }
        else if (save.DamagesUpgrades < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerDamages = 0.2f;
            save.DamagesUpgrades++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }

        UpdatePriceText(damagePriceText, save.DamagesUpgrades);
    }

    public void BuyAttackSpeedUpgrades()
    {
        if (save.AttackSpeedUpgrades < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.PlayerAttackSpeed = 0.05f;
            save.AttackSpeedUpgrades++;
            save.SaveData();
        }
        else if (save.AttackSpeedUpgrades < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerAttackSpeed = 0.1f;
            save.AttackSpeedUpgrades++;
            save.SaveData();
        }
        else if (save.AttackSpeedUpgrades < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerAttackSpeed = 0.15f;
            save.AttackSpeedUpgrades++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }

        UpdatePriceText(atkSpeedPriceText, save.AttackSpeedUpgrades);
    }

    public void BuySpeedUpgrades()
    {
        if (save.SpeedUpgrades < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.PlayerMoveSpeed = 5.3f;
            save.SpeedUpgrades++;
            save.SaveData();
        }
        else if (save.SpeedUpgrades < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerMoveSpeed = 5.6f;
            save.SpeedUpgrades++;
            save.SaveData();
        }
        else if (save.SpeedUpgrades < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerMoveSpeed = 6f;
            save.SpeedUpgrades++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }

        UpdatePriceText(speedPriceText, save.SpeedUpgrades);
    }

    /*public void BuyHealthRegenUpgrade()
    {
        if (save.HealthRegen < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.PlayerRegen = 1;
            save.HealthRegen++;
            save.SaveData();
        }
        else if (save.HealthRegen < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerRegen = 2;
            save.HealthRegen++;
            save.SaveData();
        }
        else if (save.HealthRegen < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerRegen = 3;
            save.HealthRegen++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }

        UpdatePriceText(regenPriceText, save.HealthRegen);
    }*/

    public void BuyInvincibilityFrameUpgrade()
    {
        if (save.InvincibilityFrame < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.PlayerFrame = 0.4f;
            save.InvincibilityFrame++;
            save.SaveData();
        }
        else if (save.InvincibilityFrame < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerFrame = 0.6f;
            save.InvincibilityFrame++;
            save.SaveData();
        }
        else if (save.InvincibilityFrame < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerFrame = 0.8f;
            save.InvincibilityFrame++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
        // Pas de maj du texte ici
    }

    public void BuyCritChanceUpgrade()
    {
        if (save.CritChance < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.PlayerCritchance = 10;
            save.CritChance++;
            save.SaveData();
        }
        else if (save.CritChance < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerCritchance = 20;
            save.CritChance++;
            save.SaveData();
        }
        else if (save.CritChance < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerCritchance = 30;
            save.CritChance++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }

        UpdatePriceText(critChancePriceText, save.CritChance);
    }

    public void BuyCritDamageUpgrade()
    {
        if (save.CritDamage < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.PlayerCritDamages = 25;
            save.CritDamage++;
            save.SaveData();
        }
        else if (save.CritDamage < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerCritDamages = 50;
            save.CritDamage++;
            save.SaveData();
        }
        else if (save.CritDamage < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerCritDamages = 75;
            save.CritDamage++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }

        UpdatePriceText(critDamagePriceText, save.CritDamage);
    }

    public void BuySoulsAttractUpgrade()
    {
        if (save.SoulsAttract < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.PlayerSoulsA = 1;
            save.SoulsAttract++;
            save.SaveData();
        }
        else if (save.SoulsAttract < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerSoulsA = 2;
            save.SoulsAttract++;
            save.SaveData();
        }
        else if (save.SoulsAttract < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerSoulsA = 3;
            save.SoulsAttract++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
        // Pas de maj du texte ici
    }

    public void BuyLuckUpgrade()
    {
        if (save.Luck < 1 && save.totalSouls >= price1)
        {
            audioo.Play();
            save.totalSouls -= price1;
            save.PlayerLuck = 10;
            save.Luck++;
            save.SaveData();
        }
        else if (save.Luck < 2 && save.totalSouls >= price2)
        {
            audioo.Play();
            save.totalSouls -= price2;
            save.PlayerLuck = 20;
            save.Luck++;
            save.SaveData();
        }
        else if (save.Luck < 3 && save.totalSouls >= price3)
        {
            audioo.Play();
            save.totalSouls -= price3;
            save.PlayerLuck = 30;
            save.Luck++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }

        UpdatePriceText(luckPriceText, save.Luck);
    }
}
