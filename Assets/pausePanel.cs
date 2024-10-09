using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pausePanel : MonoBehaviour
{
    public GameObject panelPause;
    private void Start()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = !panelPause.activeSelf;
            panelPause.SetActive(isActive);
            if (isActive)
            {
                Cursor.visible = true;
                Time.timeScale = 0f;

            }
            else
            {
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
        }
    }

    public void Continue()
    {
        panelPause.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
