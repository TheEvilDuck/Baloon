using System;
using UnityEngine;
using LeaderBoard;

public class MainMenuMediator: IDisposable
{
    private SceneLoader _sceneLoader;
    private MainMenuUI _mainMenuUI;
    private LeaderBoardLoader _leaderBoardLoader;

    public MainMenuMediator(SceneLoader sceneLoader, MainMenuUI mainMenuUI, LeaderBoardLoader leaderBoardLoader)
    {
        _sceneLoader = sceneLoader;
        _mainMenuUI = mainMenuUI;
        _leaderBoardLoader = leaderBoardLoader;

        _mainMenuUI.startButtonPressed+=OnStartButtonPressed;
        _mainMenuUI.exitButtonPressed+=OnExitButtonPressed;
        _leaderBoardLoader.gotErrorInResponse+=OnLeaderboardLoadFailed;
        _leaderBoardLoader.leaderBoardLoaded+=OnLeaderBoardLoaded;
    }

    public void Dispose()
    {
        _mainMenuUI.startButtonPressed-=OnStartButtonPressed;
        _mainMenuUI.exitButtonPressed-=OnExitButtonPressed;
        _leaderBoardLoader.gotErrorInResponse-=OnLeaderboardLoadFailed;
        _leaderBoardLoader.leaderBoardLoaded-=OnLeaderBoardLoaded;
    }

    private void OnStartButtonPressed() => _sceneLoader.LoadGameplay();

    private void OnExitButtonPressed() => Application.Quit();
    private void OnLeaderboardLoadFailed() => _mainMenuUI.SpawnMessage("Can't load leaderboard :(");
    private void OnLeaderBoardLoaded() => _mainMenuUI.LoadLeaderboardText(_leaderBoardLoader.CurrentLeaderBoard);
}
