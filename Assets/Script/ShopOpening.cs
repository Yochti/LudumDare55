using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpening : MonoBehaviour
{
    public GameObject PanelShop;
    public GameObject HealthBar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isActive = !PanelShop.activeSelf; 
            PanelShop.SetActive(isActive);

            if (isActive)
            {
                Time.timeScale = 0f;
                HealthBar.SetActive(false);
                Cursor.visible = true;

            }
            else
            {
                Cursor.visible = false;
                HealthBar.SetActive(true);

                Time.timeScale = 1f; 
            }
        }
    }
}
