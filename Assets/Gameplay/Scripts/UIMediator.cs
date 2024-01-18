using System;
using Gameplay;
using UnityEngine;

public class UIMediator : IDisposable
{
    private UI _uI;
    private SceneLoader _sceneLoader;
    private PlayerInput _playerInput;
    private PlayerStats _playerStats;
    private BreathController _breathController;
    private BaloonSpawner _baloonSpawner;
    private LeaderBoardLoader _leaderBoardLoader;

    public UIMediator(UI uI, SceneLoader sceneLoader, PlayerInput playerInput, PlayerStats playerStats, BreathController breathController, BaloonSpawner baloonSpawner, LeaderBoardLoader leaderBoardLoader)
    {
        _uI = uI;
        _sceneLoader = sceneLoader;
        _playerInput = playerInput;
        _playerStats = playerStats;
        _breathController = breathController;
        _baloonSpawner = baloonSpawner;
        _leaderBoardLoader = leaderBoardLoader;

        _playerStats.pointsChanged+=OnPointsChanged;

        _uI.exitButtonPressed+=OnExitButtonPressed;
        _uI.reloadButtonPressed+=OnReloadButtonPressed;
        _uI.enoughButtonPressed+=OnEnoughButtonPressed;
        _uI.submitButtonPressed+=OnSubmitButtonPressed;

        _playerInput.tapStarted+=OnInputTapStarted;
        _playerInput.tapEnded+=OnInputTapEnded;

        _breathController.breathStarted+=OnBreathStarted;

        _baloonSpawner.baloonSpawned+=OnBaloonSpawned;
    }

    public void Dispose()
    {
        _playerStats.pointsChanged-=OnPointsChanged;

        _uI.exitButtonPressed-=OnExitButtonPressed;
        _uI.reloadButtonPressed-=OnReloadButtonPressed;
        _uI.enoughButtonPressed-=OnEnoughButtonPressed;
        _uI.submitButtonPressed-=OnSubmitButtonPressed;

        _playerInput.tapStarted-=OnInputTapStarted;
        _playerInput.tapEnded-=OnInputTapEnded;

        _breathController.breathStarted-=OnBreathStarted;

        _baloonSpawner.baloonSpawned-=OnBaloonSpawned;
    }

    private void OnBaloonSpawned() => _baloonSpawner.CurrentBaloon.exploded+=OnBaloonExploded;

    private void OnBaloonExploded()
    {
        _uI.ShowResultMenu();
        BlockInput();
    }

    private void OnInputTapStarted() => _uI.ShowInhaleBar();
    private void OnInputTapEnded() => _uI.HideInhaleBar();
    private void OnExitButtonPressed() => _sceneLoader.LoadMainMenu();
    private void OnReloadButtonPressed() => _sceneLoader.ReloderCurrentScene();
    private void OnEnoughButtonPressed() => BlockInput();
    private void OnSubmitButtonPressed()  => _leaderBoardLoader.SetNewEntry(_uI.EnteredPlayerName, Mathf.FloorToInt(_playerStats.Points));
    private void OnPointsChanged(float points) => _uI.UpdatePointsText(Mathf.FloorToInt(points));
    private void OnBreathStarted() => _uI.HideInhaleBar();
    private void BlockInput() => _playerInput.tapStarted-=OnInputTapStarted;
}
