using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int health;
    public static float additionalDamage;
    public static float attackSpeed;
    public static int healthRegen;
    public static float moveSpeed;
    public static float invincibilityFrame;
    public static int critRate;
    public static float critDamage;
    public static float luck;
    public saveSytem save;
    public static float xpModifier;
    public static float dashCooldownR;
    public static float superCooldownR;
    public static int healthRegenWaves;
    private bool statsInitialized = false;

    private void Awake()
    {
        save.LoadData();
    }

    private void OnEnable()
    {
        if (!statsInitialized)
        {
            LoadPlayerStats();
            statsInitialized = true;
        }
    }
    
    private void LoadPlayerStats()
    {
        health = save.PlayerHealth;
        if (save.PlayerHealth == 0)
        {
            health = 150;
        }
        additionalDamage = save.PlayerDamages;
        print("Player damage bonus: " + save.PlayerDamages);

        attackSpeed = save.PlayerAttackSpeed;
        healthRegen = save.PlayerRegen;
        moveSpeed = save.PlayerMoveSpeed;

        invincibilityFrame = save.PlayerFrame;
        if (save.InvincibilityFrame == 0)
        {
            invincibilityFrame = 0.4f;  // Valeur par défaut
        }

        critRate = save.PlayerCritchance;
        if (save.PlayerCritchance == 0)
        {
            critRate = 15;  // Valeur par défaut
        }

        critDamage = save.PlayerCritDamages;
        if (save.PlayerCritDamages == 0)
        {
            critDamage = 200f;  // Valeur par défaut
        }
    }

    public static void additionalDmg(float addDmg)
    {
        additionalDamage += addDmg;

    }
    public static void addCrit(int critRateAdd, float critDmgAdd)
    {
        critRate += critRateAdd;
        critDamage += critDmgAdd;

    }
    
}
