using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HigherScore : MonoBehaviour
{
    public int HighScore;
    public TextMeshProUGUI myName;
    void Update()
    {
        if(Score.ScoreCount > HighScore)
        {
            HighScore = Score.ScoreCount;
        }
    }
    public void SendScore()
    {
        PlayerPrefs.SetInt("highscore", HighScore);
        HighScores.UploadScore(myName.text, HighScore);
    }
}
