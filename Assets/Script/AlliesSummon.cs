using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesSummon : MonoBehaviour
{
    public PlayerSoulsCollect Souls;
    public GameObject bababoy;
    public GameObject namee;
    public GameObject JunkTrap;
    public GameObject heiMERDEinger;
    public GameObject anivia;
    private int price = 100;

    public void Bababoy()
    {
        if(Souls.soulValue >= 100)
        {
            bababoy.SetActive(true);
            Souls.soulValue -= price;
        }

    }
    public void Name()
    {
        if (Souls.soulValue >= 100)
        {
            namee.SetActive(true);
            Souls.soulValue -= price;

        }

    }
    public void jUNKTRAP()
    {
        if (Souls.soulValue >= 100)
        {
            JunkTrap.SetActive(true);
            Souls.soulValue -= price;

        }

    }
    public void HeiMERDEinger()
    {
        if (Souls.soulValue >= 100)
        {
            heiMERDEinger.SetActive(true);
            Souls.soulValue -= price;

        }

    }
    public void Anivia()
    {
        if (Souls.soulValue >= 100)
        {
            anivia.SetActive(true);
            Souls.soulValue -= price;

        }

    }

}
