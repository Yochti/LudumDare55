using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlliesSummon : MonoBehaviour
{
    // script
    public PlayerHealth playerHealth;
    public Ally1Controller ally1;
    public AllyHealer ally2;
    public Ally3Controller ally3;
    public Ally4Controller ally4;
    public BulletLife bulletAlly;
    public HealBullet bulletH;
    public portal1 p1;
    public PortalDivision p2;

    // GameObject
    public GameObject player;
    public GameObject friendHolder;

    public GameObject bababoy;
    public GameObject bababoy2;
    public GameObject namee;
    public GameObject JunkTrap;
    public GameObject heiMERDEinger;
    public GameObject Dimitri;
    public GameObject hera;

    // Status ally
    public string bababoyStatus;
    public string nameStatus;
    public string junktrapStatus;
    public string hStatus;
    public string dimitriStatus;
    public int heraStatus;
    public bool lvlToRevive;

    private void Start()
    {
        bababoyStatus = "0";
        nameStatus = "0";
        junktrapStatus = "0";
        hStatus = "0";
        dimitriStatus = "0";
        heraStatus = 0;
    }

    public void Bababoy()
    {
        Time.timeScale = 1f;

        if (bababoyStatus == "0")
        {
            ActivateBababoy(1);
        }
        else if (bababoyStatus == "1")
        {
            UpgradeBababoy(2);
        }
        else if (bababoyStatus == "2")
        {
            FinalUpgradeBababoy(3);
        }
    }

    private void ActivateBababoy(int level)
    {
        bababoy.SetActive(true);
        friendHolder.transform.position = player.transform.position;
        bababoyStatus = level.ToString();
    }

    private void UpgradeBababoy(int level)
    {
        bababoyStatus = level.ToString();
        ally1.shootingCooldown = 0.2f;
        ally1.bulletSpeed = 30f;
        bulletAlly.bulletDamage += 5;
    }

    private void FinalUpgradeBababoy(int level)
    {
        bababoy2.SetActive(true);
        bababoy.SetActive(false);
        bababoyStatus = level.ToString();
    }

    public void Name()
    {
        Time.timeScale = 1f;

        if (nameStatus == "0")
        {
            ActivateName(1);
        }
        else if (nameStatus == "1")
        {
            UpgradeName(2);
        }
        else if (nameStatus == "2")
        {
            FinalUpgradeName(3);
        }
    }

    private void ActivateName(int level)
    {
        namee.SetActive(true);
        PlayerHealth.maxHealth += 25;
        playerHealth.currentHealth += 25;
        nameStatus = level.ToString();
    }

    private void UpgradeName(int level)
    {
        nameStatus = level.ToString();
        PlayerHealth.maxHealth += 25;
        playerHealth.currentHealth += 25;
        ally2.healingInterval = 19;
    }

    private void FinalUpgradeName(int level)
    {
        nameStatus = level.ToString();
        lvlToRevive = true;
        ally2.canRevive = true;
    }

    public void JunkTrape()
    {
        Time.timeScale = 1f;

        if (junktrapStatus == "0")
        {
            ActivateJunkTrap(1);
        }
        else if (junktrapStatus == "1")
        {
            UpgradeJunkTrap(2);
        }
        else if (junktrapStatus == "2")
        {
            FinalUpgradeJunkTrap(3);
        }
    }

    private void ActivateJunkTrap(int level)
    {
        JunkTrap.SetActive(true);
        friendHolder.transform.position = player.transform.position;
        junktrapStatus = level.ToString();
    }

    private void UpgradeJunkTrap(int level)
    {
        junktrapStatus = level.ToString();
        ally3.trapPlacementInterval -= 6;
    }

    private void FinalUpgradeJunkTrap(int level)
    {
        junktrapStatus = level.ToString();
    }

    public void HeiMERDEinger()
    {
        Time.timeScale = 1f;

        if (hStatus == "0")
        {
            ActivateHeiMERDEinger(1);
        }
        else if (hStatus == "1")
        {
            UpgradeHeiMERDEinger(2);
        }
        else if (hStatus == "2")
        {
            FinalUpgradeHeiMERDEinger(3);
        }
    }

    private void ActivateHeiMERDEinger(int level)
    {
        heiMERDEinger.SetActive(true);
        friendHolder.transform.position = player.transform.position;
        hStatus = level.ToString();
    }

    private void UpgradeHeiMERDEinger(int level)
    {
        hStatus = level.ToString();
        ally4.maxTurrets = 5;
        bulletH.healAmount += 1;
        bulletAlly.bulletDamage += 5;
    }

    private void FinalUpgradeHeiMERDEinger(int level)
    {
        hStatus = level.ToString();
    }

    public void DimitriVoid()
    {
        Time.timeScale = 1f;

        if (dimitriStatus == "0")
        {
            ActivateDimitri(1);
        }
        else if (dimitriStatus == "1")
        {
            UpgradeDimitri(2);
        }
        else if (dimitriStatus == "2")
        {
            FinalUpgradeDimitri(3);
        }
    }

    private void ActivateDimitri(int level)
    {

        Dimitri.SetActive(true);
        friendHolder.transform.position = player.transform.position;
        dimitriStatus = level.ToString();
    }

    private void UpgradeDimitri(int level)
    {
        dimitriStatus = level.ToString();
        p2.numberOfBullet += 2;
    }

    private void FinalUpgradeDimitri(int level)
    {
        dimitriStatus = level.ToString();
    }

    public void HeraVoid()
    {
        Time.timeScale = 1f;
        if (heraStatus == 0)
        {
            ActivateHera(1);
        }
        else if (heraStatus == 1)
        {
            UpgradeHera(2);
        }
        else if (heraStatus == 2)
        {
            FinalUpgradeHera(3);
        }
    }

    private void ActivateHera(int level)
    {
        hera.SetActive(true);
        friendHolder.transform.position = player.transform.position;
        heraStatus = level;
    }

    private void UpgradeHera(int level)
    {
        heraStatus = level;
        PlayerGrowZone.damageMultiplier = 2f;
        HealZone.healAmount = 2;
    }

    private void FinalUpgradeHera(int level)
    {
        heraStatus = level;
    }
}

