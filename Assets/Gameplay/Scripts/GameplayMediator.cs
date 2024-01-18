using System;
using PlayerInput;

public class GameplayMediator: IDisposable
{
    private BaloonSpawner _baloonSpawner;
    private PlayerStatsCalculator _playerStatsCalculator;
    private IPlayerInput _playerInput;
    private BreathController _breathController;

    public GameplayMediator(BaloonSpawner baloonSpawner, PlayerStatsCalculator playerStatsCalculator, IPlayerInput playerInput, BreathController breathController)
    {
        _baloonSpawner = baloonSpawner;
        _playerStatsCalculator = playerStatsCalculator;
        _playerInput = playerInput;
        _breathController = breathController;

        _baloonSpawner.baloonSpawned+=OnBaloonSpawned;

        _playerInput.mainActionStarted+=OnInputTapStarted;
        _playerInput.mainActionEnded+=OnInputTapEnded;
        
        _breathController.breathStarted+=OnBreathStarted;
        _breathController.breathEnded+=OnBreathEnded;
    }

    public void Dispose()
    {
        _baloonSpawner.baloonSpawned-=OnBaloonSpawned;

        _playerInput.mainActionStarted-=OnInputTapStarted;
        _playerInput.mainActionEnded-=OnInputTapEnded;

        if (_baloonSpawner.CurrentBaloon!=null)
        {
            _baloonSpawner.CurrentBaloon.exploded-=OnBaloonExploded;
            _baloonSpawner.CurrentBaloon.grown-=OnBaloonGrown;
        }

        _breathController.breathStarted-=OnBreathStarted;
        _breathController.breathEnded-=OnBreathEnded;
    }

    private void OnBaloonSpawned()
    {
        _baloonSpawner.CurrentBaloon.exploded+=OnBaloonExploded;
        _baloonSpawner.CurrentBaloon.grown+=OnBaloonGrown;
    }

    private void OnBaloonExploded()
    {
        _playerStatsCalculator.ClearPoints();
        _breathController.StopBreath();
        BlockInput();
    }

    private void OnBaloonGrown() => _playerStatsCalculator.AddPoints();
    private void OnInputTapEnded() => _breathController.StopBreath();
    private void OnInputTapStarted() => _breathController.StartBreath();
    private void OnBreathStarted() => _baloonSpawner.CurrentBaloon.StartGrow();
    private void OnBreathEnded() => _baloonSpawner.CurrentBaloon.StopGrow();
    private void BlockInput() => _playerInput.mainActionStarted-=OnInputTapStarted;
}
