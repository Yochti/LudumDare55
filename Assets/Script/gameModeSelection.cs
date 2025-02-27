using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameModeSelection : MonoBehaviour
{
    public void twentyWaves()
    {
        SceneManager.LoadScene(4);
    }
    public void twentyMinutes()
    {
        SceneManager.LoadScene(5);
    } 
    public void Endless()
    {
        SceneManager.LoadScene("Endless");
    }
}
