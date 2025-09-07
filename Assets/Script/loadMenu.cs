using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class loadMenu : MonoBehaviour
{
    public TextMeshProUGUI wavesText;
    public saveSytem save;
    private void OnEnable()
    {
        int i = staticRef.wavesS + 1;
        wavesText.text = "Wave:" + i.ToString();
    }
    public void loadSceneMain()
    {
        save.totalSouls += PlayerHealth.finalSouls;
        save.SaveData();
        SceneManager.LoadScene(0);
    }
}
