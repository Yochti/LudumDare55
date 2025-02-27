using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addStats : MonoBehaviour
{
    public GameObject panelPowerUp;
    public void closePanel()
    {
        panelPowerUp.SetActive(false);
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
}
