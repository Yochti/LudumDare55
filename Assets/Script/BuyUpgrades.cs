using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUpgrades : MonoBehaviour
{
    // References
    public saveSytem save;
    // Prices
    public int price1 = 1250;
    public int price2 = 5000;
    public int price3 = 10000;
    public AudioSource audioo;  // Son de succès d'achat
    public AudioSource audioo2; // Son d'échec d'achat

    public void BuyHealthUpgrades()
    {
        if (save.HealthUpgrades < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.TotalSouls -= price1;
            save.HealthUpgrades++;
            save.PlayerHealth = 250;
            save.SaveData();
        }
        else if (save.HealthUpgrades < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.TotalSouls -= price2;
            save.HealthUpgrades++;
            save.PlayerHealth = 300;
            save.SaveData();
        }
        else if (save.HealthUpgrades < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.TotalSouls -= price3;
            save.HealthUpgrades++;
            save.PlayerHealth = 400;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    public void BuyDamageUpgrades()
    {
        if (save.DamagesUpgrades < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.TotalSouls -= price1;
            save.DamagesUpgrades++;
            save.PlayerDamages = 0.1f;
            save.SaveData();
        }
        else if (save.DamagesUpgrades < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.TotalSouls -= price2;
            save.PlayerDamages = 0.2f;
            save.DamagesUpgrades++;
            save.SaveData();
        }
        else if (save.DamagesUpgrades < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.TotalSouls -= price3;
            save.PlayerDamages = 0.3f;
            save.DamagesUpgrades++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    public void BuyAttackSpeedUpgrades()
    {
        if (save.AttackSpeedUpgrades < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.TotalSouls -= price1;
            save.PlayerAttackSpeed = 0.1f;
            save.AttackSpeedUpgrades++;
            save.SaveData();
        }
        else if (save.AttackSpeedUpgrades < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.PlayerAttackSpeed = 0.2f;
            save.TotalSouls -= price2;
            save.AttackSpeedUpgrades++;
            save.SaveData();
        }
        else if (save.AttackSpeedUpgrades < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.PlayerAttackSpeed = 0.3f;
            save.TotalSouls -= price3;
            save.AttackSpeedUpgrades++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    public void BuySpeedUpgrades()
    {
        if (save.SpeedUpgrades < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.PlayerMoveSpeed = 5.5f;
            save.TotalSouls -= price1;
            save.SpeedUpgrades++;
            save.SaveData();
        }
        else if (save.SpeedUpgrades < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.PlayerMoveSpeed = 6f;
            save.TotalSouls -= price2;
            save.SpeedUpgrades++;
            save.SaveData();
        }
        else if (save.SpeedUpgrades < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.PlayerMoveSpeed = 7f;
            save.TotalSouls -= price3;
            save.SpeedUpgrades++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    public void BuyHealthRegenUpgrade()
    {
        if (save.HealthRegen < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.PlayerRegen = 1;
            save.TotalSouls -= price1;
            save.HealthRegen++;
            save.SaveData();
        }
        else if (save.HealthRegen < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.PlayerRegen = 2;
            save.TotalSouls -= price2;
            save.HealthRegen++;
            save.SaveData();
        }
        else if (save.HealthRegen < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.PlayerRegen = 3;
            save.TotalSouls -= price3;
            save.HealthRegen++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    public void BuyInvincibilityFrameUpgrade()
    {
        if (save.InvincibilityFrame < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.PlayerFrame = 0.5f;
            save.TotalSouls -= price1;
            save.InvincibilityFrame++;
            save.SaveData();
        }
        else if (save.InvincibilityFrame < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.PlayerFrame = 0.7f;
            save.TotalSouls -= price2;
            save.InvincibilityFrame++;
            save.SaveData();
        }
        else if (save.InvincibilityFrame < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.PlayerFrame = 1f;
            save.TotalSouls -= price3;
            save.InvincibilityFrame++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    public void BuyCritChanceUpgrade()
    {
        if (save.CritChance < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.PlayerCritchance = 20;
            save.TotalSouls -= price1;
            save.CritChance++;
            save.SaveData();
        }
        else if (save.CritChance < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.PlayerCritchance = 30;
            save.TotalSouls -= price2;
            save.CritChance++;
            save.SaveData();
        }
        else if (save.CritChance < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.PlayerCritchance = 50;
            save.TotalSouls -= price3;
            save.CritChance++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    public void BuyCritDamageUpgrade()
    {
        if (save.CritDamage < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.PlayerCritDamages = 220;
            save.TotalSouls -= price1;
            save.CritDamage++;
            save.SaveData();
        }
        else if (save.CritDamage < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.PlayerCritDamages = 250;
            save.TotalSouls -= price2;
            save.CritDamage++;
            save.SaveData();
        }
        else if (save.CritDamage < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.PlayerCritDamages = 300;
            save.TotalSouls -= price3;
            save.CritDamage++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    public void BuySoulsAttractUpgrade()
    {
        if (save.SoulsAttract < 1 && save.TotalSouls >= price1)
        {
            audioo.Play();
            save.PlayerSoulsA = 1;
            save.TotalSouls -= price1;
            save.SoulsAttract++;
            save.SaveData();
        }
        else if (save.SoulsAttract < 2 && save.TotalSouls >= price2)
        {
            audioo.Play();
            save.PlayerSoulsA = 2;
            save.TotalSouls -= price2;
            save.SoulsAttract++;
            save.SaveData();
        }
        else if (save.SoulsAttract < 3 && save.TotalSouls >= price3)
        {
            audioo.Play();
            save.PlayerSoulsA = 3;
            save.TotalSouls -= price3;
            save.SoulsAttract++;
            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }
}
