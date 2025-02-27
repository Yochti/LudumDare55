using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausePanel : MonoBehaviour
{
    public GameObject panelPause; 
    public GameObject optionPanel;  
    private int windowCount = 0;   

    private void Start()
    {
        panelPause.SetActive(false);
        optionPanel.SetActive(false);
        Time.timeScale = 1f;
        windowCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscPress();
        }
    }

    private void HandleEscPress()
    {
        if (windowCount == 0)
        {
            panelPause.SetActive(true);
            windowCount = 1;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else if (windowCount == 1)
        {
            panelPause.SetActive(false);
            windowCount = 0;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else if (windowCount == 2)
        {
            optionPanel.SetActive(false);
            windowCount = 1;
        }
    }

    public void OptionMenu()
    {
        if (!optionPanel.activeSelf)
        {
            optionPanel.SetActive(true);
            windowCount = 2;
        }
    }

    public void Continue()
    {
        panelPause.SetActive(false);
        optionPanel.SetActive(false);
        windowCount = 0;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
