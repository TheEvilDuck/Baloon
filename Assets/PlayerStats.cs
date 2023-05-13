using System;
using UnityEngine;

public class PlayerStats
{
    public float points {get;private set;}
    public event Action<float>pointsChanged;
    private float _pointsPerStep;
    private float _holdMultiplier;
    private float _currentMultiplier = 1f;
    public PlayerStats(float pointePerStep, float holdMultiplier)
    {
        _pointsPerStep = pointePerStep;
        _holdMultiplier = holdMultiplier;
    }
    public void OnBaloonGrow()
    {
        points+=_pointsPerStep*_currentMultiplier;
        _currentMultiplier+=_holdMultiplier;
        pointsChanged?.Invoke(points);
    }
    public void OnBaloonExploded()
    {
        points = 0;
        pointsChanged?.Invoke(points);
    }
    public void OnGrowStop()
    {
        _currentMultiplier = 1f;
    }
}
