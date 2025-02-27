using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActiveObject : MonoBehaviour
{
    public GameObject panelGameMode;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelGameMode.SetActive(false);
        }
    }
}
