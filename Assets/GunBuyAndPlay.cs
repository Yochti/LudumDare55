using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunBuyAndPlay : MonoBehaviour
{
    public saveSytem save;
    public Button gun1sprite;
    public Button gun2sprite;
    public Button gun3sprite;
    public Button gun4sprite;
    public Button gun5sprite;
    public Button gun6sprite;
    public Button gun7sprite;
    public Button gun8sprite;
    public Color baseColor;

    // VALEUR DES ACHATS A CHANGER (ACTUELLEMENT A 0 POUR LES TESTS) !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private void Update()
    {
        if (save.whichWeapon == "Gun1")
        {
            gun1sprite.GetComponent<Image>().color = Color.green;
            gun2sprite.GetComponent<Image>().color = baseColor;
            gun3sprite.GetComponent<Image>().color = baseColor;
            gun4sprite.GetComponent<Image>().color = baseColor;
            gun5sprite.GetComponent<Image>().color = baseColor;
            gun6sprite.GetComponent<Image>().color = baseColor;
            gun7sprite.GetComponent<Image>().color = baseColor;
            gun8sprite.GetComponent<Image>().color = baseColor;
            gun8sprite.GetComponent<Image>().color = baseColor;




        }
        else if (save.whichWeapon == "Gun2")
        {
            gun1sprite.GetComponent<Image>().color = baseColor;
            gun2sprite.GetComponent<Image>().color = Color.green;
            gun3sprite.GetComponent<Image>().color = baseColor;
            gun4sprite.GetComponent<Image>().color = baseColor;
            gun5sprite.GetComponent<Image>().color = baseColor;
            gun6sprite.GetComponent<Image>().color = baseColor;
            gun7sprite.GetComponent<Image>().color = baseColor;
            gun8sprite.GetComponent<Image>().color = baseColor;

        }
        else if (save.whichWeapon == "Gun3")
        {
            gun1sprite.GetComponent<Image>().color = baseColor;
            gun2sprite.GetComponent<Image>().color = baseColor;
            gun3sprite.GetComponent<Image>().color = Color.green;
            gun4sprite.GetComponent<Image>().color = baseColor;
            gun5sprite.GetComponent<Image>().color = baseColor;
            gun6sprite.GetComponent<Image>().color = baseColor;
            gun7sprite.GetComponent<Image>().color = baseColor;
            gun8sprite.GetComponent<Image>().color = baseColor;

        }
        else if (save.whichWeapon == "Gun4")
        {
            gun1sprite.GetComponent<Image>().color = baseColor;
            gun2sprite.GetComponent<Image>().color = baseColor;
            gun3sprite.GetComponent<Image>().color = baseColor;
            gun4sprite.GetComponent<Image>().color = Color.green;
            gun5sprite.GetComponent<Image>().color = baseColor;
            gun6sprite.GetComponent<Image>().color = baseColor;
            gun7sprite.GetComponent<Image>().color = baseColor;
            gun8sprite.GetComponent<Image>().color = baseColor;

        }
        else if (save.whichWeapon == "Gun5")
        {
            gun1sprite.GetComponent<Image>().color = baseColor;
            gun2sprite.GetComponent<Image>().color = baseColor;
            gun3sprite.GetComponent<Image>().color = baseColor;
            gun4sprite.GetComponent<Image>().color = baseColor;
            gun5sprite.GetComponent<Image>().color = Color.green;
            gun6sprite.GetComponent<Image>().color = baseColor;
            gun7sprite.GetComponent<Image>().color = baseColor;
            gun8sprite.GetComponent<Image>().color = baseColor;

        }
        else if (save.whichWeapon == "Gun6")
        {
            gun1sprite.GetComponent<Image>().color = baseColor;
            gun2sprite.GetComponent<Image>().color = baseColor;
            gun3sprite.GetComponent<Image>().color = baseColor;
            gun4sprite.GetComponent<Image>().color = baseColor;
            gun5sprite.GetComponent<Image>().color = baseColor;
            gun6sprite.GetComponent<Image>().color = Color.green;
            gun7sprite.GetComponent<Image>().color = baseColor;
            gun8sprite.GetComponent<Image>().color = baseColor;

        }
        else if (save.whichWeapon == "Gun7")
        {
            gun1sprite.GetComponent<Image>().color = baseColor;
            gun2sprite.GetComponent<Image>().color = baseColor;
            gun3sprite.GetComponent<Image>().color = baseColor;
            gun4sprite.GetComponent<Image>().color = baseColor;
            gun5sprite.GetComponent<Image>().color = baseColor;
            gun6sprite.GetComponent<Image>().color = baseColor;
            gun7sprite.GetComponent<Image>().color = Color.green;
            gun8sprite.GetComponent<Image>().color = baseColor;

        }
        else if (save.whichWeapon == "Gun8")
        {
            gun1sprite.GetComponent<Image>().color = baseColor;
            gun2sprite.GetComponent<Image>().color = baseColor;
            gun3sprite.GetComponent<Image>().color = baseColor;
            gun4sprite.GetComponent<Image>().color = baseColor;
            gun5sprite.GetComponent<Image>().color = baseColor;
            gun6sprite.GetComponent<Image>().color = baseColor;
            gun7sprite.GetComponent<Image>().color = baseColor;
            gun8sprite.GetComponent<Image>().color = Color.green;
        }
    }
    public void Gun1()
    {
        save.whichWeapon = "Gun1";
        save.SaveData();
    }

    public void Gun2()
    {
        if (!save.hasGun2 && save.TotalSouls >= 0)
        {
            save.hasGun2 = true;
            save.TotalSouls -= 0;
        }
        else
        {
            save.whichWeapon = "Gun2";

        }
        save.SaveData();
    }

    public void Gun3()
    {
        if (!save.hasGun3 && save.TotalSouls >= 0)
        {
            save.hasGun3 = true;
            save.TotalSouls -= 0;
        }
        else
        {
            save.whichWeapon = "Gun3";

        }
        save.SaveData();
    }

    public void Gun4()
    {
        if (!save.hasGun4 && save.TotalSouls >= 0)
        {
            save.hasGun4 = true;
            save.TotalSouls -= 0;
        }
        else
        {

            save.whichWeapon = "Gun4";

        }
        save.SaveData();
    }

    public void Gun5()
    {
        if (!save.hasGun5 && save.TotalSouls >= 0)
        {
            save.hasGun5 = true;
            save.TotalSouls -= 0;
        }
        else
        {
            save.whichWeapon = "Gun5";
        }
        save.SaveData();
    }

    public void Gun6()
    {
        if (!save.hasGun6 && save.TotalSouls >= 0)
        {
            save.hasGun6 = true;
            save.TotalSouls -= 0;
        }
        else
        {
            save.whichWeapon = "Gun6";
        }
        save.SaveData();
    }

    public void Gun7()
    {
        if (!save.hasGun7 && save.TotalSouls >= 0)
        {
            save.hasGun7 = true;
            save.TotalSouls -= 0;
        }
        else
        {
            save.whichWeapon = "Gun7";

        }
        save.SaveData();
    }

    public void Gun8()
    {
        if (!save.hasGun8 && save.TotalSouls >= 0)
        {
            save.hasGun8 = true;
            save.TotalSouls -= 10000;
        }
        else
        {
            save.whichWeapon = "Gun8";

        }
        save.SaveData();
    }
}
