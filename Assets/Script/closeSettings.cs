using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeSettings : MonoBehaviour
{
    public GameObject settingsMenu;

    public void closeSettingse()
    {
        settingsMenu.SetActive(false);
    }
}
