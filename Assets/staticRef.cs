using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticRef : MonoBehaviour
{
    public EnemyWaveManager waves;
    public static int wavesS;
    private void Update()
    {
        wavesS = waves.currentWave;
    }
}
