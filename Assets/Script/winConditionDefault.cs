using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winConditionDefault : MonoBehaviour
{
    public EnemyWaveManager waveManager;
    public saveSytem save;
    public GameObject player;
    public GameObject winPanel;
    private void Update()
    {
        if(waveManager.currentWave > waveManager.wavesCount)
        {
            save.whichGameMode = "Endless";
            Time.timeScale = 0f;
            player.SetActive(false);
            winPanel.SetActive(true);
        }
    }
}
