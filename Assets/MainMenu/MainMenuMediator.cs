using System;
using UnityEngine;

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

        _mainMenuUI.LoadLeaderboardText(_leaderBoardLoader.Get());
    }

    public void Dispose()
    {
        _mainMenuUI.startButtonPressed-=OnStartButtonPressed;
        _mainMenuUI.exitButtonPressed-=OnExitButtonPressed;
    }

    private void OnStartButtonPressed() => _sceneLoader.LoadGameplay();

    private void OnExitButtonPressed() => Application.Quit();
}
