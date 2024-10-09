using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatsEndGame : MonoBehaviour
{
    [Header("Player")]
    public TextMeshProUGUI killPlayer;
    public TextMeshProUGUI damagePlayer;

    [Header("Bababoy")]
    public TextMeshProUGUI killBababoy;
    public TextMeshProUGUI damageBababoy;

    [Header("Name")]
    public TextMeshProUGUI healName;

    [Header("Junktrap")]
    public TextMeshProUGUI damageJunktrap;
    public TextMeshProUGUI killJunktrap;

    [Header("H")]
    public TextMeshProUGUI damageH;
    public TextMeshProUGUI killH;
    public TextMeshProUGUI healH;

    [Header("Dimitri")]
    public TextMeshProUGUI damageDimitri;
    public TextMeshProUGUI killDimitri;

    public playerBulletDamage pDamage;
    public bulletBababoyDamage bDmg;
    public killAmountStats kStats;

    public EnemyWaveManager WaveInt;
    public TextMeshProUGUI WaveText;

    private void Update()
    {
        //DAMAGE
        damagePlayer.text = pDamage.PlayerDamageInt.ToString();
        damageBababoy.text = bDmg.BababoyDamageInt.ToString();
        damageDimitri.text = pDamage.dimitriDamage.ToString();
        damageH.text = pDamage.totalHDamage.ToString();
        damageJunktrap.text = pDamage.totalTDamage.ToString();

        //HEAL
        healName.text = pDamage.healAmoutName.ToString();
        healH.text = pDamage.healAmoutTurret.ToString();
        //KILL
        killPlayer.text = kStats.killPlayer.ToString();
        killBababoy.text = kStats.killBababoy.ToString();
        killJunktrap.text = kStats.totalkillJunktrap.ToString();
        killH.text = kStats.totalkillH.ToString();
        killDimitri.text = kStats.totalKillDimitri.ToString();

        WaveText.text = (WaveInt.currentWave + 1).ToString();
    }
}
