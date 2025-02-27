using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSoulsGestion : MonoBehaviour
{
    public GameObject Trigger0;
    public GameObject Trigger1;
    public GameObject Trigger2;
    public GameObject Trigger3;
    public saveSytem save;

    private void Start()
    {
        if(save.PlayerSoulsA == 1)
        {
            Trigger1.SetActive(true);
        }
        if(save.PlayerSoulsA == 2)
        {
            Trigger2.SetActive(true);
        }
        if(save.PlayerSoulsA == 3)
        {
            Trigger3.SetActive(true);
        }
        else
        {
            Trigger0.SetActive(true);
        }
    }
}
