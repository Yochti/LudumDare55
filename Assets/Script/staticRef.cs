using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticRef : MonoBehaviour
{
    public EnemyWaveManager waves;
    public static int wavesS;
    public GameObject addPoints;
    public static int pointUpgrade;
    public TMPro.TextMeshProUGUI pointScore;
    private void Start()
    {
        wavesS = 0;
        pointUpgrade = 0;
    }
    public void addPoint()
    {
        pointUpgrade++;
        pointScore.text = pointUpgrade.ToString() + "/3";
        StartCoroutine(setActiveImage());
    }
    IEnumerator setActiveImage()
    {
        addPoints.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        addPoints.SetActive(false);
    }
    private void Update()
    {
        wavesS = waves.currentWave;
    }
}
