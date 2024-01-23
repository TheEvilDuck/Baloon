using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using Dan.Models;
using System.Threading.Tasks;

public class LeaderBoardLoader
{
    private const string KEY = "f73c27ba5056430878395ac6fd4854d169c2bd46297b8961d2430a0ac86b75eb";
    private const string SECRET_KEY = "480617e83752404bf91e5473ac4fb30b0645a0dd7cccdf7bd90fae9706af6497bb245dd44bb5c5b2168a313139dc4ac5f0e749147b0f0a5ccfcc61c2321dbc8407b6ad92688eb5244e9800fa9c05699f141adfd706c39c2851c2f3063971bfd19bf1c83c7e8a9457e256361fed7e918ca5093441d2fa736ee14c001b3e522aa6";
    private Entry[] _currentLeaderboard;

    public void SetNewEntry(string name, int score)
    {
        if (name==string.Empty)
            return;
        LeaderboardCreator.UploadNewEntry(KEY,name,score,OnNewEntryLoaded);
    }

    public Entry[] Get() => _currentLeaderboard;

    public async Task Load()
    {
        LeaderboardCreator.GetLeaderboard(KEY,false, OnLeaderboardGotSuccess);

        while (_currentLeaderboard==null)
           await Task.Delay(100);

    }

    private void OnLeaderboardGotSuccess(Entry[] entries)
    {
        _currentLeaderboard = entries;
    }

    private void OnNewEntryLoaded(bool success)
    {

    }
}
