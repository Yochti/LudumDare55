using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayMaintittle : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject gameModePanel;
   

    public void chooseGameMode()
    {
        gameModePanel.SetActive(true);
    }
    public void backtomainmenuFromGameMode()
    {
        gameModePanel.SetActive(false);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void optionPanelOpen()
    {
        optionPanel.SetActive(true);
    }
    public void optionPanelClose()
    {
        optionPanel.SetActive(false);
    }

}
