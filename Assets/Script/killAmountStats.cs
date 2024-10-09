using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killAmountStats : MonoBehaviour
{
    [Header("Player")]
    public int killPlayer;
    [Header("Bababoy")]
    public int killBababoy;
    [Header("Junktrap")]
    public int trap3kill;
    public int mineTrapKill;
    public int totalkillJunktrap;
    [Header("H")]
    public int turret1kill;
    public int turet3kill;
    public int totalkillH;
    [Header("Dimitri")]
    public int fois3kill;
    public int explosiveBullet;
    public int totalKillDimitri;

    public void IncreaseKillPlayer()
    {
        killPlayer++;
    }
    public void IncreaseBababoyKill()
    {
        killBababoy++;
    }
    public void IncreaseMineKill()
    {
        mineTrapKill++;
        totalkillJunktrap++;
    }
    public void IncreaseTrap3Kill()
    {
        trap3kill++;
        totalkillJunktrap++;
    }
    public void Increaseturret1kill()
    {
        turret1kill++;
        totalkillH++;
    }
    public void IncreaseTuret3Kill()
    {
        turet3kill++;
        totalkillH++;
    }
    public void IncreaseFois3Kill()
    {
        fois3kill++;
        totalKillDimitri++;
    }
    public void IncreaseExplosive()
    {
        explosiveBullet++;
        totalKillDimitri++;
    }
    
}
