using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using Dan.Models;
using System.Threading.Tasks;

public class LeaderBoardLoader
{
    private const string KEY = "76a504f121b3ae2af38aa6920d4f35824196cf4a9b5a8fced1cda1d1a688d43e";
    private Entry[] _currentLeaderboard;

    public void SetNewEntry(string name, int score)
    {
        if (name==string.Empty)
            return;

        LeaderboardCreator.UploadNewEntry(KEY,name,score,OnNewEntryLoaded);
    }

    public Entry[] Get() => _currentLeaderboard;

    public void Load() => LeaderboardCreator.GetLeaderboard(KEY,true, OnLeaderboardGotSuccess);

    private void OnLeaderboardGotSuccess(Entry[] entries) => _currentLeaderboard = entries;

    private void OnNewEntryLoaded(bool success)
    {

    }
}
