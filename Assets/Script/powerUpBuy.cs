using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpBuy : MonoBehaviour
{
    public GameObject[] hache;
    public int lvlS = 0;
    public GameObject[] laser;
    public int lvlLa;

    public GameObject[] poison;
    public int lvlP;
    public int lvlvAxes;
    public int lvlBulletRain;
    public int lvlvF;
    public GameObject panelPowerUp;
    public int lvlAddDmg;
    public int lvlCrit;
    public void Sickle()
    {
        if (lvlS < 1)
        {
            hache[0].SetActive(true);
        }
        else if (lvlS < 2)
        {
            hache[1].SetActive(true);
        }
        else if (lvlS < 3)
        {
            hache[2].SetActive(true);
        }
        else if (lvlS < 4)
        {
            hache[3].SetActive(true);
        }
        else
            return;
        lvlS++;
        Cursor.visible = false;
        panelPowerUp.SetActive(false);
        PowerUpPanel.numberRoll++;
        Time.timeScale = 1f;
    }
    public void Laser()
    {
        if (lvlLa < 1)
        {
            laser[0].SetActive(true);
        }
        else if (lvlLa < 2)
        {
            laser[1].SetActive(true);
        }
        else if (lvlLa < 3)
        {
            laser[2].SetActive(true);
        }
        else if (lvlLa < 4)
        {
            laser[3].SetActive(true);
        }
        else
            return;
        PowerUpPanel.numberRoll++;
        panelPowerUp.SetActive(false);
        Time.timeScale = 1f;
        lvlLa++;
        Cursor.visible = false;

    }


    public void Poison()
    {
        if (lvlP > 0 )
        {
            poison[lvlP - 1].SetActive(false); 
        }
        poison[lvlP].SetActive(true);
        panelPowerUp.SetActive(false);
        Time.timeScale = 1f;
        lvlP++;
        Cursor.visible = false;
        PowerUpPanel.numberRoll++;


    }

    public void Axes()
    {
        if(lvlvAxes < 1)
        {
            ProjectileSpawner.numberOfProjectiles = 2;
        }
        else if(lvlvAxes < 2)
        {
            ProjectileSpawner.numberOfProjectiles = 3;
        }
        else if(lvlvAxes < 3)
        {
            ProjectileSpawner.numberOfProjectiles = 4;
        }
        else if (lvlvAxes < 4)
        {
            ProjectileSpawner.numberOfProjectiles = 5;
        }
        else
            return;
        lvlvAxes++;
        panelPowerUp.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        PowerUpPanel.numberRoll++;


    }
    public void BulletRain()
    {
        if(lvlBulletRain < 1)
        {
            ProjectileRain.numberOfProjectiles = 4;
        }
        else if(lvlBulletRain < 2)
        {
            ProjectileRain.numberOfProjectiles = 6;
        }
        else if(lvlBulletRain < 3)
        {
            ProjectileRain.numberOfProjectiles = 8;
        }
        else if (lvlBulletRain < 4)
        {
            ProjectileRain.numberOfProjectiles = 10;
        }
        else
            return;
        lvlBulletRain++;
        panelPowerUp.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        PowerUpPanel.numberRoll++;


    }
    public void Flame()
    {
        if(lvlvF < 1)
        {
            FlameZoneInstanciate.number = 2;
        }
        else if(lvlvF < 2)
        {
            FlameZoneInstanciate.number = 3;
        }
        else if(lvlvF < 3)
        {
            FlameZoneInstanciate.number = 4;
        }
        else if (lvlvF < 4)
        {
            FlameZoneInstanciate.number = 5;
        }
        else
            return;
        lvlvF++;
        panelPowerUp.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        PowerUpPanel.numberRoll++;


    }


    public void addDamages()
    {
        if (lvlAddDmg < 10)
        {
            PlayerStats.additionalDmg(.1f);
            lvlAddDmg++;

        }
        else return;
        panelPowerUp.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        PowerUpPanel.numberRoll++;

    }
    public void addCrit()
    {
        if (lvlCrit < 4)
        {
            PlayerStats.addCrit(7, 12.5f);
            lvlCrit++;

        }
        else return;
        panelPowerUp.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        PowerUpPanel.numberRoll++;

    }

}
