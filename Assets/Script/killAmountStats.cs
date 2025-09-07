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


    public static int totalKill;
    private void Start()
    {
        totalKill = 0;
    }
    public void IncreaseKillPlayer()
    {
        killPlayer++;
        increaseTotal();
    }
    public void IncreaseBababoyKill()
    {
        killBababoy++;
        increaseTotal();
    }
    public void IncreaseMineKill()
    {
        mineTrapKill++;
        totalkillJunktrap++;
        increaseTotal();
    }
    public void IncreaseTrap3Kill()
    {
        trap3kill++;
        totalkillJunktrap++;
        increaseTotal();
    }
    public void Increaseturret1kill()
    {
        turret1kill++;
        totalkillH++;
        increaseTotal();
    }
    public void IncreaseTuret3Kill()
    {
        turet3kill++;
        totalkillH++;
        increaseTotal();
    }
    public void IncreaseFois3Kill()
    {
        fois3kill++;
        totalKillDimitri++;
        increaseTotal();
    }
    public void IncreaseExplosive()
    {
        explosiveBullet++;
        totalKillDimitri++;
        increaseTotal();
    }
    private void increaseTotal()
    {
        totalKill++;
    }
    
}
