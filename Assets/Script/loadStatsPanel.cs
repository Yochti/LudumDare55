using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadStatsPanel : MonoBehaviour
{
    public GameObject leaderboard;
    public GameObject Stats;
    public void OnclickButtonStats()
    {
        leaderboard.SetActive(false);
        Stats.SetActive(true);
    }
    public void OnclickButtonLeaderboard()
    {
        leaderboard.SetActive(true);
        Stats.SetActive(false);
    }
    
}
