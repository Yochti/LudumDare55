using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingGeneral : MonoBehaviour
{
    public saveSytem save;
    
    public void ToggleAutoShoot(bool isAutoshoot)
    {
        save.isAutoShoot = isAutoshoot;
        save.SaveData();
    }
    public void ToggleAutoAim(bool isAutoAim)
    {
        save.isAutoAim = isAutoAim;
        save.SaveData();
    }
}
