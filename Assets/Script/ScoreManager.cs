using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    public static Score scoree;
    public TextMeshProUGUI finalScoreText;
    public TMP_InputField inputName;
    public UnityEvent<string, int> submittScoreEvent;
    private void Update()
    {
        finalScoreText.text = Score.ScoreCount.ToString();
    }
    public void SubmitScore()
    {
        submittScoreEvent.Invoke(inputName.text, int.Parse(finalScoreText.text));    
    }

}
