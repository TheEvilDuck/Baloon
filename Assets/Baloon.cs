using UnityEngine;
using System;

public class Baloon : MonoBehaviour
{
    private bool _grow = false;
    private Transform _transform;
    private float _step = 0.01f;
    private float _currentGrow = 1f;
    private float _maxSizeInStartSizePercent = 10f;
    private Vector3 _startSize;

    public event Action grown;
    public event Action exploded;

    private Game _game;
    private void OnDisable() 
    {
       if (_game!=null)
        {
            _game.playerBreathStarted-=OnBreathStarted;
            _game.playerBreathEnded-=OnBreathEnded;
        }
    }

    private void OnBreathStarted()
    {
        _grow = true;
    }
    private void OnBreathEnded()
    {
        _grow = false;
    }

    private void Awake() 
    {
        _transform = transform;
        _startSize = _transform.localScale;
    }

    private void Grow()
    {
        _currentGrow+=_step;
        if (_currentGrow>=_maxSizeInStartSizePercent)
        {
            exploded?.Invoke();
        }
        else
        {
            _transform.localScale = _startSize*_currentGrow;
            grown?.Invoke();
        }
    }
    void Update()
    {
        if (_grow)
            Grow();
    }

    public void SetValues(float growStep,float maxSize, Game game)
    {
        _step = maxSize*growStep;
        _maxSizeInStartSizePercent = maxSize;
        _game = game;
        _game.playerBreathStarted+=OnBreathStarted;
        _game.playerBreathEnded+=OnBreathEnded;

    }
}
