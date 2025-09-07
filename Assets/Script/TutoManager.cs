using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public GameObject TutoEmpty;
    void Start()
    {
        Cursor.visible = true;
    }
    private void Update()
    {
        if (TutoEmpty.activeSelf) Time.timeScale = 0f;
    }
    public void okWithResetTimeScale(GameObject panelTuto)
    {
        panelTuto.SetActive(false);
        TutoEmpty.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
    public void okayWithoutResetTimeScale(GameObject nextTuto)
    {
        nextTuto.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 1f;

    }

}
