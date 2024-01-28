using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using LootLocker.Requests;
using System;

namespace LeaderBoard
{
    public class LeaderBoardLoader
    {
        private const int LEADERBOARD_SIZE = 10;
        private const string LEADERBOARD_KEY = "baloonKey";
        private const int INIT_WAIT_MAX_TIME = 60;
        private PlayerData[] _currentLeaderboard;
        private string _sessionId;
        private string _playerId;
        private string _currentPlayerName;
        private bool _scoreSubmitted = false;
        private bool _response = false;
        private bool _loaded = false;
        private bool _canceled = false;

        public event Action leaderboardLoadFailed;

        public string CurrentPlayerName => _currentPlayerName;

        public LeaderBoardLoader()
        {
            LootLockerSDKManager.StartGuestSession(OnSessionStarted);
        }

        public async Task<bool> SendNewScore(int score)
        {
            _scoreSubmitted = false;
            _response = false;
            LootLockerSDKManager.SubmitScore(_sessionId,score,LEADERBOARD_KEY,OnScoreSubmit);

            while (!_response)
                await Task.Delay(100);

            return _scoreSubmitted;
        }

        public IEnumerable<PlayerData> Get() => _currentLeaderboard;

        public async Task Load()
        {
            _canceled = false;

            for (int i = 0; i< INIT_WAIT_MAX_TIME;i++)
            {
                if (!LootLockerSDKManager.CheckInitialized()&&!_canceled)
                    await Task.Delay(100);
                else
                    break;
            }

            if (!LootLockerSDKManager.CheckInitialized()||_canceled)
            {
                leaderboardLoadFailed?.Invoke();
                return;
            }

            LootLockerSDKManager.GetScoreList(LEADERBOARD_KEY,LEADERBOARD_SIZE,OnLeaderBorardLoadComplete);

            while (!_loaded)
                await Task.Delay(100);

            LootLockerSDKManager.GetMemberRank(LEADERBOARD_KEY,_playerId, (response)=>{
                if (response.success)
                {
                    _currentPlayerName = response.player.name;
                }
            });
        }
        public void ChangePlayerName(string name)
        {
            LootLockerSDKManager.SetPlayerName(name,OnPlayerNameChanged);
        }

        public void CancelLoading() => _canceled = true;

        private void OnLeaderBorardLoadComplete(LootLockerGetScoreListResponse response)
        {
            if (!response.success)
            {
                Debug.LogError(response.errorData.message);
            }
            else
            {
                Debug.Log("LeaderBoard loaded successfully!");

                List<PlayerData>loadedLeaderBoard = new List<PlayerData>();
                foreach (LootLockerLeaderboardMember item in response.items)
                {
                    PlayerData playerData = new PlayerData(item.player.name,item.score);
                    loadedLeaderBoard.Add(playerData);

                }

                _currentLeaderboard = loadedLeaderBoard.ToArray();
            }

            _loaded = true;
        }

        private void OnSessionStarted(LootLockerGuestSessionResponse response)
        {
            if (!response.success)
            {
                Debug.LogError(response.errorData.message);
            }
            else
            {
                Debug.Log($"Lootlocker session started! Player id: {response.player_identifier}");

                _sessionId = response.player_identifier;
                _playerId = response.player_id.ToString();
            }

            

        }

        private void OnScoreSubmit(LootLockerSubmitScoreResponse response)
        {
            if (!response.success)
            {
                Debug.LogError(response.errorData.message);
            }
            else
            {
                Debug.Log("New score submited successfully!");
                _scoreSubmitted = true;
            }

            _response = true;
        }

        private void OnPlayerNameChanged(PlayerNameResponse response)
        {
            if (!response.success)
            {
                Debug.LogError(response.errorData.message);
            }
            else
            {
                Debug.Log("Player name successfully changed!");
            }
        }
    }

}