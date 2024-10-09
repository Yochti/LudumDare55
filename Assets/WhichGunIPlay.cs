using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichGunIPlay : MonoBehaviour
{
    public saveSytem save;

    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Gun3;
    public GameObject Gun4;
    public GameObject Gun5;
    public GameObject Gun6;
    public GameObject Gun7; 
    public GameObject Gun8;

    void Start()
    {
        if (save.whichWeapon == "Gun1")
        {
            Gun1.SetActive(true);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            Gun4.SetActive(false);
            Gun5.SetActive(false);
            Gun6.SetActive(false);
            Gun7.SetActive(false);
            Gun8.SetActive(false);
        }
        else if (save.whichWeapon == "Gun2")
        {
            Gun1.SetActive(false);
            Gun2.SetActive(true);
            Gun3.SetActive(false);
            Gun4.SetActive(false);
            Gun5.SetActive(false);
            Gun6.SetActive(false);
            Gun7.SetActive(false);
            Gun8.SetActive(false);
        }
        else if (save.whichWeapon == "Gun3")
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(true);
            Gun4.SetActive(false);
            Gun5.SetActive(false);
            Gun6.SetActive(false);
            Gun7.SetActive(false);
            Gun8.SetActive(false);
        }
        else if (save.whichWeapon == "Gun4")
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            Gun4.SetActive(true);
            Gun5.SetActive(false);
            Gun6.SetActive(false);
            Gun7.SetActive(false);
            Gun8.SetActive(false);
        }
        else if (save.whichWeapon == "Gun5")
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            Gun4.SetActive(false);
            Gun5.SetActive(true);
            Gun6.SetActive(false);
            Gun7.SetActive(false);
            Gun8.SetActive(false);
        }
        else if (save.whichWeapon == "Gun6")
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            Gun4.SetActive(false);
            Gun5.SetActive(false);
            Gun6.SetActive(true);
            Gun7.SetActive(false);
            Gun8.SetActive(false);
        }
        else if (save.whichWeapon == "Gun7")
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            Gun4.SetActive(false);
            Gun5.SetActive(false);
            Gun6.SetActive(false);
            Gun7.SetActive(true);
            Gun8.SetActive(false);
        }
        else if (save.whichWeapon == "Gun8")
        {
            Gun1.SetActive(false);
            Gun2.SetActive(false);
            Gun3.SetActive(false);
            Gun4.SetActive(false);
            Gun5.SetActive(false);
            Gun6.SetActive(false);
            Gun7.SetActive(false);
            Gun8.SetActive(true);
        }
    }
}
