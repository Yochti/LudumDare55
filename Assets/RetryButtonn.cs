using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryButtonn : MonoBehaviour
{
    public GameObject panelDeath;
    private void Start()
    {
        panelDeath.SetActive(false);

    }
    public void retry()
    {
        SceneManager.LoadScene(1);
        panelDeath.SetActive(true);
        Score.ScoreCount = 0;
    }
    private void Update()
    {
        bool isActive = !panelDeath.activeSelf;
        if (isActive)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;

    }


}
