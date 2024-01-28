using System;
using Gameplay;
using PlayerInput;
using UnityEngine;
using LeaderBoard;
using System.Threading.Tasks;

public class UIMediator : IDisposable
{
    private UI _uI;
    private SceneLoader _sceneLoader;
    private IPlayerInput _playerInput;
    private PlayerStats _playerStats;
    private BreathController _breathController;
    private BaloonSpawner _baloonSpawner;
    private LeaderBoardLoader _leaderBoardLoader;

    public UIMediator(UI uI, SceneLoader sceneLoader, IPlayerInput playerInput, PlayerStats playerStats, BreathController breathController, BaloonSpawner baloonSpawner, LeaderBoardLoader leaderBoardLoader)
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

        _playerInput.mainActionStarted+=OnInputTapStarted;
        _playerInput.mainActionEnded+=OnInputTapEnded;

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

        _playerInput.mainActionStarted-=OnInputTapStarted;
        _playerInput.mainActionEnded-=OnInputTapEnded;

        _breathController.breathStarted-=OnBreathStarted;

        _baloonSpawner.baloonSpawned-=OnBaloonSpawned;
    }

    private void OnBaloonSpawned() => _baloonSpawner.CurrentBaloon.exploded+=OnBaloonExploded;

    private void OnBaloonExploded()
    {
        _uI.ShowResultMenu();
        _uI.HideSubmit();
        _uI.HideInhaleBar();
        BlockInput();
    }

    private void OnInputTapStarted() => _uI.ShowInhaleBar();
    private void OnInputTapEnded() => _uI.HideInhaleBar();
    private void OnExitButtonPressed() => _sceneLoader.LoadMainMenu();
    private void OnReloadButtonPressed() => _sceneLoader.ReloderCurrentScene();
    private void OnEnoughButtonPressed()
    {
        _uI.UpdateName(_leaderBoardLoader.CurrentPlayerName);
        BlockInput();
        _uI.HideInhaleBar();

        if (_playerStats.Points==0)
            _uI.HideSubmit();
    }
    private async Task<bool> OnSubmitButtonPressed()
    {
        bool success =await _leaderBoardLoader.SendNewScore(Mathf.FloorToInt(_playerStats.Points));

        if (success)
        {
            _leaderBoardLoader.ChangePlayerName(_uI.EnteredPlayerName);
            return true;
        }

        return false;
    }
    private void OnPointsChanged(float points) => _uI.UpdatePointsText(Mathf.FloorToInt(points));
    private void OnBreathStarted() => _uI.HideInhaleBar();
    private void BlockInput()
    {
        _playerInput.Block();
        _breathController.StopBreath();
    } 

}
