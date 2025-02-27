using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichGunIPlay : MonoBehaviour
{
    public saveSytem save;

    public GameObject[] Guns;

    void Start()
    {
        string selectedWeapon = save.whichWeapon;

        if (string.IsNullOrEmpty(selectedWeapon))
        {
            selectedWeapon = "Gun1";
        }

        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].SetActive("Gun" + (i + 1) == selectedWeapon);
        }
    }
}
