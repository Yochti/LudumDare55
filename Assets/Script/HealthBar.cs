using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public saveSytem save;
    public GameObject tutoPanel;
    private void Start()
    {
        save.LoadData();
        if (!save.firstFight)
        {
            tutoPanel.SetActive(true);
            save.firstFight = true;
            save.SaveData();

        }


    }

}
