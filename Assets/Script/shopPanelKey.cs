using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopPanelKey : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject nextPanel;
    public GameObject currentPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentPanel.SetActive(false);
            previousPanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentPanel.SetActive(false);
            nextPanel.SetActive(true);
        }
    }
}
