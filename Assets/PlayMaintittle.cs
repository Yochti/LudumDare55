using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayMaintittle : MonoBehaviour
{
    public GameObject tutoPanel;
   public void playButton()
   {
        SceneManager.LoadScene(1);
   }
    public void openTuto()
    {
        tutoPanel.SetActive(true);
    }
    public void closeTuto()
    {
        tutoPanel.SetActive(false);
    }
}
