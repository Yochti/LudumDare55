using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
public class LeqderBoqrd : MonoBehaviour
{
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;
    private string publicLeaderBoardKey = "2718017d1373818c95a873eabea5c4e73e6059eba59767b02031237cb3987278";


    void Start()
    {
        GetLeaderBoard();
    }
    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey, ((msg) =>{
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();

            }
        }));
    }
    public void SetLeaderBoardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey, username, score, ((msg) =>{
            GetLeaderBoard();
        }));
    }
    

    
}
