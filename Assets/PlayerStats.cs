using System;

public class PlayerStats
{
    private float _points;
    public Action<float>pointsChanged;
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
        _points+=_pointsPerStep*_currentMultiplier;
        _currentMultiplier+=_holdMultiplier;
        pointsChanged?.Invoke(_points);
    }
    public void OnBaloonExploded()
    {
        _points = 0;
        pointsChanged?.Invoke(_points);
    }
    public void OnGrowStop()
    {
        _currentMultiplier = 1f;
    }
}
