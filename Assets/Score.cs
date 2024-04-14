using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public static int ScoreCount;

    private void Update()
    {
        ScoreText.text = "Score" + Mathf.Round(ScoreCount);
    }

}
