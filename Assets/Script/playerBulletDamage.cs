using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletDamage : MonoBehaviour
{
    [Header("Player")]
    public int PlayerDamageInt;
    [Header("Dimitri")]
    public int fois3PortalsDamage;
    public int explosionPortalsDamage;
    public int dimitriDamage;
    [Header("Name")]
    public int healAmoutName;
    [Header("H")]
    public int healAmoutTurret;
    public int dmgTurret1;
    public int dmgTurret3;
    public int totalHDamage;
    [Header("Junktrap")]
    public int dmgMine;
    public int dmgTrap3;
    public int totalTDamage;
    public void OnHit(int dAmount)
    {
        PlayerDamageInt += dAmount;
    }
    public void OnHitFois3()
    {
        PlayerDamageInt += 10;
        fois3PortalsDamage += 25;
        dimitriDamage += 25;
    }
    public void onHitTurret1(int dAmount)
    {
        dmgTurret1 += dAmount;
        totalHDamage += dAmount;
    }
    public void onHitTurret3(int dAmount)
    {
        dmgTurret3 += dAmount;
        totalHDamage += dAmount;
    }

    public void onHitExplosion(int colliderNumber)
    {
        PlayerDamageInt += 10;
        explosionPortalsDamage += (50 * colliderNumber);
        dimitriDamage += (50 * colliderNumber);
    }
    public void onHitHeal(int healAmount)
    {
        healAmoutTurret += healAmount;
    }
    public void hName(int hAmount)
    {
        healAmoutName += hAmount;
    }
    public void OnHitMine(int dAmount)
    {
        dmgMine += dAmount;
        totalTDamage += dAmount;
    }
    public void OnHitTrap(int dAmount)
    {
        dmgTrap3 += dAmount;
        totalTDamage += dAmount;

    }
    
}
