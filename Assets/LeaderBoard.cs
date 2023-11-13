using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using Dan.Models;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]int _entriesNumberToShow = 5;
    public static readonly string leaderBoardKey = "76a504f121b3ae2af38aa6920d4f35824196cf4a9b5a8fced1cda1d1a688d43e";
    [SerializeField]TextMeshProUGUI _tmproPrefab;
    private void Start() {
        LeaderboardCreator.GetLeaderboard(LeaderBoard.leaderBoardKey,true,OnLeaderboardGotSuccess);
    }
    private void OnLeaderboardGotSuccess(Entry[] entries)
    {
        for (int i = 0;i<Mathf.Min(_entriesNumberToShow,entries.Length);i++)
        {
            TextMeshProUGUI tmpro =  Instantiate(_tmproPrefab,transform);
            tmpro.text = i.ToString()+": "+ entries[i].Username+": "+entries[i].Score.ToString();
        }
    }
}
