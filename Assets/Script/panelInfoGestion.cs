using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelInfoGestion : MonoBehaviour
{

    public GameObject[] panel;


    public void Close()
    {
        for (int y = 0; y < panel.Length; y++)
        {
            panel[y].SetActive(false);
        }
    }
    public void OpenPanel(int number)
    {
        for (int i = 0; i < panel.Length; i++)
        {
            panel[i].SetActive(false);
        }
        panel[number].SetActive(true);
    }
   
}
