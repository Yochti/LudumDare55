using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayMaintittle : MonoBehaviour
{
    public GameObject optionPanel;
   public void playButton()
   {
        SceneManager.LoadScene(1);
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
