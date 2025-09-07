using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addStats : MonoBehaviour
{
    public GameObject panelPowerUp;
    public PlayerHealth playerHealth;
    public void closePanel()
    {
        panelPowerUp.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
    public void addCritR(int critRate)
    {
        PlayerStats.critRate += critRate;
        closePanel();
    }
    public void cooldownDash(float cd)
    {
        PlayerStats.dashCooldownR += cd;
        closePanel();
    }
    public void cooldownSuper(float cd)
    {
        PlayerStats.superCooldownR += cd;
        closePanel();
    }
    public void vampirePassif(int passif)
    {
        playerPassif.pVampire += passif;
        closePanel();
    }
    public void healthRegenWave(int regen)
    {
        PlayerStats.healthRegenWaves += regen;
        PowerUpWaves.Instance.RemoveChosenPowerUp("WaveRegen");
        closePanel();
    }
    public void addCritD(int critDamage)
    {
        PlayerStats.critDamage += critDamage;
        closePanel();
    }
    public void addSpeed(float speed)
    {
        PlayerStats.moveSpeed += speed;
        closePanel();
    }
    public void addDamage(float damage)
    {
        PlayerStats.additionalDamage += damage;
        closePanel();
    }
    public void addXpModifier(float xpModifier)
    {
        PlayerStats.xpModifier += xpModifier;
        closePanel();
    }
    public void addAttackSpeed(float attackSpeed)
    {
        PlayerStats.attackSpeed += attackSpeed;
        closePanel();
    }
    public void addMaxHealth(int health)
    {
        PlayerHealth.maxHealth += health;
        closePanel();
    }
    public void addLuck(int luck)
    {
        PlayerStats.luck += luck;
        closePanel();
    }
    public void Berkserker()
    {
        playerHealth.activeBerserker = true;
        PowerUpWaves.Instance.RemoveChosenPowerUp("Berserker");
        closePanel();
    }
    public void atqDmgandSpeedInfinity()
    {
        playerHealth.activeInfinity = true;
        PowerUpWaves.Instance.RemoveChosenPowerUp("InfinityAtqDmgSpeed");
        closePanel();

    }
    public void AllIn(float damage)
    {
        PlayerStats.additionalDamage += damage;
        playerHealth.damageFois += 0.3f;
        closePanel();
    }
    public void TurretAtqSpeed()
    {
        PlayerMovement.hasTurretActivate = true;
        PowerUpWaves.Instance.RemoveChosenPowerUp("TurretAtqSpeed");
        closePanel();
    }
    public void mooveSpeedWaves()
    {
        EnemyWaveManager.hasSpeedBoostWaveActivated = true;
        PowerUpWaves.Instance.RemoveChosenPowerUp("MooveSpeedWaves");
        closePanel();
    }
    public void reroll(int count)
    {
        PowerUpWaves.rerollCount += count;
        closePanel();

    }
    public void RegenKillGolem()
    {
        EnemyWaveManager.hasRegenKillGolem = true;
        PowerUpWaves.Instance.RemoveChosenPowerUp("RegenKill");
        closePanel();
    }
    public void RegenBossKill()
    {
        Boss1Health.hasRegenBossKill = true;
        PowerUpWaves.Instance.RemoveChosenPowerUp("HealthregenBossKill");
        closePanel();
    }
    public void BalanceHealth()
    {
        PlayerHealth.hasBalanceHealth = true;
        PowerUpWaves.Instance.RemoveChosenPowerUp("BalanceHealth");
        closePanel();
    }
    public void DoubleDamageFullHp()
    {
        PlayerHealth.doubleDamage = true;
        PowerUpWaves.Instance.RemoveChosenPowerUp("DoubleDamage");
        closePanel();
    }
    public void upDamageOnHit()
    {

    }
    public void instantHeal()
    {
        int healingValue = Mathf.RoundToInt((PlayerHealth.maxHealth - playerHealth.currentHealth) * 0.25f);
        playerHealth.Heal(healingValue);
        closePanel();

    }
    public void moveSpeedRegen(int regenAmount)
    {

        PlayerMovement.healthRegenMovePassif += regenAmount;
        closePanel();

    }
}
