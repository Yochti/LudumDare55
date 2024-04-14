using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpening : MonoBehaviour
{
    public GameObject PanelShop;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isActive = !PanelShop.activeSelf; 
            PanelShop.SetActive(isActive);

            if (isActive)
            {
                Time.timeScale = 0f; 
            }
            else
            {
                Time.timeScale = 1f; 
            }
        }
    }
}
