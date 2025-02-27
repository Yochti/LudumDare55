using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class lvlInvoc : MonoBehaviour
{
    public Image lvl1;
    public Image lvl2;
    public Image lvl3;
    private int lvlInvoca;
    public Color colorLvl;
    private void Start()
    {
        lvlInvoca = 0;
    }
    void Update()
    {
        if (lvlInvoca == 1) lvl1.color = colorLvl;
        else if (lvlInvoca == 2) lvl2.color = colorLvl;
        else if (lvlInvoca == 3) lvl3.color = colorLvl;
    }
    public void addLvl()
    {
        lvlInvoca++;
    }
}
