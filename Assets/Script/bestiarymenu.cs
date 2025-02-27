using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bestiarymenu : MonoBehaviour
{
    public void LoadSceneBesitary()
    {
        SceneManager.LoadScene(2);
    }    public void LoadSceneShop()
    {
        SceneManager.LoadScene(3);
    }
}
