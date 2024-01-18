using System;

public class SoundMediator: IDisposable
{
    private SoundManager _soundManager;
    private BreathController _breathController;
    private PlayerInput _playerInput;
    private BaloonSpawner _baloonSpawner;

    public SoundMediator(SoundManager soundManager, BreathController breathController, PlayerInput playerInput, BaloonSpawner baloonSpawner)
    {
        _soundManager = soundManager;
        _breathController = breathController;
        _playerInput = playerInput;
        _baloonSpawner = baloonSpawner;

        _playerInput.tapStarted+=OnInputTapStart;
        _playerInput.tapEnded+=OnInputTapEnded;

        _breathController.breathStarted+=OnBreathStarted;

        _baloonSpawner.baloonSpawned+=OnBaloonSpawned;
    }

    public void Dispose()
    {
        _playerInput.tapStarted-=OnInputTapStart;
        _playerInput.tapEnded-=OnInputTapEnded;

        _breathController.breathStarted-=OnBreathStarted;

        _baloonSpawner.baloonSpawned-=OnBaloonSpawned;

        if (_baloonSpawner.CurrentBaloon!=null)
             _baloonSpawner.CurrentBaloon.exploded-=OnBaloonExploded;
    }

    private void OnBreathStarted() => _soundManager.PlayBreathSound();
    private void OnBaloonExploded()
    {
        _soundManager.PlayExplosionSound();
        _playerInput.tapStarted-=OnInputTapStart;
        _playerInput.tapEnded-=OnInputTapEnded;
    }
    private void OnInputTapStart() => _soundManager.PlayInhaleSound();
    private void OnBaloonSpawned() => _baloonSpawner.CurrentBaloon.exploded+=OnBaloonExploded;
    private void OnInputTapEnded() => _soundManager.StopCurrentSound();

    
}
