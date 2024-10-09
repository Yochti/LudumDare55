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


    public void BuyHealthUpgrades()
    {
        if (save.HealthUpgrades < 1 && save.TotalSouls >= price1)
        {
            save.TotalSouls -= price1;
            save.HealthUpgrades++;
            save.PlayerHealth = 300;
            save.SaveData();
        }
        else if (save.HealthUpgrades < 2 && save.TotalSouls >= price2)
        {
            save.TotalSouls -= price2;
            save.HealthUpgrades++;
            save.PlayerHealth = 400;
            save.SaveData();
        }
        else if (save.HealthUpgrades < 3 && save.TotalSouls >= price3)
        {
            save.TotalSouls -= price3;
            save.HealthUpgrades++;
            save.PlayerHealth = 500;
            save.SaveData();
        }
    }

    public void BuyDamageUpgrades()
    {
        if (save.DamagesUpgrades < 1 && save.TotalSouls >= price1)
        {
            save.TotalSouls -= price1;
            save.DamagesUpgrades++;
            save.PlayerDamages = 15+ 15*(10 / 100);
            save.SaveData();
        }
        else if (save.DamagesUpgrades < 2 && save.TotalSouls >= price2)
        {
            save.TotalSouls -= price2;
            save.PlayerDamages = 15 + 15 * (20 / 100);

            save.DamagesUpgrades++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.DamagesUpgrades < 3 && save.TotalSouls >= price3)
        {
            save.TotalSouls -= price3;
            save.PlayerDamages = 15 + 15 * (30 / 100);
            save.DamagesUpgrades++; // Incrementer le compteur global
            save.SaveData();
        }
    }

    public void BuyAttackSpeedUpgrades()
    {
        if (save.AttackSpeedUpgrades < 1 && save.TotalSouls >= price1)
        {
            save.TotalSouls -= price1;
            save.PlayerAttackSpeed = 0.4f;
            save.AttackSpeedUpgrades++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.AttackSpeedUpgrades < 2 && save.TotalSouls >= price2)
        {
            save.PlayerAttackSpeed = 0.35f;
            save.TotalSouls -= price2;
            save.AttackSpeedUpgrades++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.AttackSpeedUpgrades < 3 && save.TotalSouls >= price3)
        {
            save.PlayerAttackSpeed = 0.3f;
            save.TotalSouls -= price3;
            save.AttackSpeedUpgrades++; // Incrementer le compteur global
            save.SaveData();
        }
    }

    public void BuySpeedUpgrades()
    {
        if (save.SpeedUpgrades < 1 && save.TotalSouls >= price1)
        {
            save.PlayerMoveSpeed = 5.5f;
            save.TotalSouls -= price1;
            save.SpeedUpgrades++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.SpeedUpgrades < 2 && save.TotalSouls >= price2)
        {
            save.PlayerMoveSpeed = 6f;
            save.TotalSouls -= price2;
            save.SpeedUpgrades++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.SpeedUpgrades < 3 && save.TotalSouls >= price3)
        {
            save.PlayerMoveSpeed = 7f;
            save.TotalSouls -= price3;
            save.SpeedUpgrades++; // Incrementer le compteur global
            save.SaveData();
        }
    }

    public void BuyHealthRegenUpgrade()
    {
        if (save.HealthRegen < 1 && save.TotalSouls >= price1)
        {
            save.PlayerRegen = 1;
            save.TotalSouls -= price1;
            save.HealthRegen++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.HealthRegen < 2 && save.TotalSouls >= price2)
        {
            save.PlayerRegen = 2;
            save.TotalSouls -= price2;
            save.HealthRegen++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.HealthRegen < 3 && save.TotalSouls >= price3)
        {
            save.PlayerRegen = 3;
            save.TotalSouls -= price3;
            save.HealthRegen++; // Incrementer le compteur global
            save.SaveData();
        }
    }

    public void BuyInvincibilityFrameUpgrade()
    {
        if (save.InvincibilityFrame < 1 && save.TotalSouls >= price1)
        {
            save.PlayerFrame = 0.5f;
            save.TotalSouls -= price1;
            save.InvincibilityFrame++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.InvincibilityFrame < 2 && save.TotalSouls >= price2)
        {
            save.PlayerFrame = 0.7f;
            save.TotalSouls -= price2;
            save.InvincibilityFrame++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.InvincibilityFrame < 3 && save.TotalSouls >= price3)
        {
            save.PlayerFrame = 1f;
            save.TotalSouls -= price3;
            save.InvincibilityFrame++; // Incrementer le compteur global
            save.SaveData();
        }
    }

    public void BuyCritChanceUpgrade()
    {
        if (save.CritChance < 1 && save.TotalSouls >= price1)
        {
            save.PlayerCritchance = 20;
            save.TotalSouls -= price1;
            save.CritChance++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.CritChance < 2 && save.TotalSouls >= price2)
        {
            save.PlayerCritchance = 30;
            save.TotalSouls -= price2;
            save.CritChance++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.CritChance < 3 && save.TotalSouls >= price3)
        {
            save.PlayerCritchance = 50;
            save.TotalSouls -= price3;
            save.CritChance++; // Incrementer le compteur global
            save.SaveData();
        }
    }

    public void BuyCritDamageUpgrade()
    {
        if (save.CritDamage < 1 && save.TotalSouls >= price1)
        {
            save.PlayerCritDamages = 220;
            save.TotalSouls -= price1;
            save.CritDamage++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.CritDamage < 2 && save.TotalSouls >= price2)
        {
            save.PlayerCritDamages = 250;
            save.TotalSouls -= price2;
            save.CritDamage++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.CritDamage < 3 && save.TotalSouls >= price3)
        {
            save.PlayerCritDamages = 300;
            save.TotalSouls -= price3;
            save.CritDamage++; // Incrementer le compteur global
            save.SaveData();
        }
    }

    public void BuySoulsAttractUpgrade()
    {
        if (save.SoulsAttract < 1 && save.TotalSouls >= price1)
        {
            save.PlayerSoulsA = 1;
            save.TotalSouls -= price1;
            save.SoulsAttract++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.SoulsAttract < 2 && save.TotalSouls >= price2)
        {
            save.PlayerSoulsA = 2;
            save.TotalSouls -= price2;
            save.SoulsAttract++; // Incrementer le compteur global
            save.SaveData();
        }
        else if (save.SoulsAttract < 3 && save.TotalSouls >= price3)
        {
            save.PlayerSoulsA = 3;
            save.TotalSouls -= price3;
            save.SoulsAttract++; // Incrementer le compteur global
            save.SaveData();
        }
    }
}