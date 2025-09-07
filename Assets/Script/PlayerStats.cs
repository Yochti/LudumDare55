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
    public static int critRate = 25;
    public static float critDamage = 175f;
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
        if (save.PlayerMoveSpeed == 0) moveSpeed = 5f;
        else moveSpeed = save.PlayerMoveSpeed;

        invincibilityFrame = save.PlayerFrame;
        if (save.InvincibilityFrame == 0)
        {
            invincibilityFrame = 0.4f;  // Valeur par défaut
        }

        critRate += save.PlayerCritchance;

        if (save.Luck == 0) luck = 0;
        xpModifier = 0;
        critDamage += save.PlayerCritDamages;

    }

    public static void additionalDmg(float addDmg)
    {
        additionalDamage += addDmg;

    }
    private void Update()
    {
        print(additionalDamage);
    }
    public static void addCrit(int critRateAdd, float critDmgAdd)
    {
        critRate += critRateAdd;
        critDamage += critDmgAdd;

    }
    
}
