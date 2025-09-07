using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipGun : MonoBehaviour
{
    public saveSytem save;
    public Image backgroundColor;
    public Color equippedColor;
    public Color baseColor;
    public string nameWeapon;

    private void Update()
    {
        if (nameWeapon == save.whichWeapon)
        {
            backgroundColor.color = equippedColor;
        }
        else
        {
            backgroundColor.color = baseColor;
        }
    }

    public void EquipGun()
    {
        if (IsWeaponOwned(nameWeapon))
        {
            save.whichWeapon = nameWeapon;
            save.SaveData();
        }
        else
        {
            Debug.Log("Cette arme n'est pas encore débloquée.");
        }
    }

    private bool IsWeaponOwned(string gunName)
    {
        return gunName == "Gun1" ||
               (gunName == "Gun3" && save.hasGun3) ||
               (gunName == "Gun4" && save.hasGun4) ||
               (gunName == "Gun6" && save.hasGun6) ||
               (gunName == "Gun7" && save.hasGun7);
    }
}
