using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using LootLocker.Requests;
using System;
using LootLocker;

namespace LeaderBoard
{
    public class LeaderBoardLoader
    {
        private const int LEADERBOARD_SIZE = 10;
        private const string LEADERBOARD_KEY = "baloonKey";
        private const int INIT_WAIT_MAX_TIME = 5;

        public event Action gotErrorInResponse;
        public event Action gotSuccessResponseAfterSentScore;
        public event Action leaderBoardLoaded;

        private PlayerData[] _currentLeaderboard;
        private string _playerId;

        public IEnumerable<PlayerData> CurrentLeaderBoard => _currentLeaderboard;
        public string CurrentPlayerName {get; private set;}

        public async void Load()
        {
            for (int i = 0;i<INIT_WAIT_MAX_TIME;i++)
                if (!LootLockerSDKManager.CheckInitialized())
                    await Task.Delay(1000);
                else
                    break;

            if (!LootLockerSDKManager.CheckInitialized())
            {
                gotErrorInResponse?.Invoke();
                Debug.LogError("Can't initilize lootlocker SDK");
                return;
            }

            LootLockerSDKManager.StartGuestSession(OnSessionStarted);
        }

        public void SendNewScore(int score)
        {
            if (!LootLockerSDKManager.CheckInitialized())
            {
                Debug.LogError("Lootlocker SDK isn't initilized! Trying again...");
                Load();
                return;
            }

            LootLockerSDKManager.SubmitScore(_playerId,score,LEADERBOARD_KEY,OnSentNewScore);
        }

        public void UpdatePlayerName(string newName)
        {
            LootLockerSDKManager.SetPlayerName(newName,OnGotPlayerName);
        }

        private bool CheckResponseForError(LootLockerResponse response)
        {
            if (response.success)
                return false;

            Debug.LogError(response.errorData.message);
            gotErrorInResponse?.Invoke();


            return true;
        }

        private void OnSessionStarted(LootLockerGuestSessionResponse response)
        {
            if (CheckResponseForError(response))
                return;

            _playerId = response.public_uid;
            LootLockerSDKManager.GetPlayerName(OnGotPlayerName);
            LootLockerSDKManager.GetScoreList(LEADERBOARD_KEY,LEADERBOARD_SIZE,OnLeaderBoardLoaded);
        }

        private void OnLeaderBoardLoaded(LootLockerGetScoreListResponse response)
        {
            if (CheckResponseForError(response))
                return;

            List<PlayerData>result = new List<PlayerData>();

            foreach (var item in response.items)
                result.Add(new PlayerData(item.player.name,item.score));

            _currentLeaderboard = result.ToArray();

            leaderBoardLoaded?.Invoke();
            Debug.Log("A");
        }

        private void OnSentNewScore (LootLockerSubmitScoreResponse response)
        {
            if (CheckResponseForError(response))
                return;

            Debug.Log("Score hase successfully been sent");
            gotSuccessResponseAfterSentScore?.Invoke();
        }

        private void OnGotPlayerName(PlayerNameResponse response)
        {
            if (CheckResponseForError(response))
                return;

            CurrentPlayerName = response.name;
        }

    }

}