public class PlayerStatsCalculator
{
    private readonly PlayerStats _playerStats;
    private readonly float _pointsPerStep;
    private readonly float _holdMultiplier;

    private float _currentMultiplier = 1f;

    public PlayerStatsCalculator(PlayerStats playerStats, BaloonConfig config)
    {
        _playerStats = playerStats;

        _pointsPerStep = config.PointsPerStep;
        _holdMultiplier = config.HoldMultiplier;
    }

    public void AddPoints() => _playerStats.ChangePoints(_playerStats.Points+_pointsPerStep*_currentMultiplier);

    public void ClearPoints() => _playerStats.ChangePoints(0);

    public void IncreaseMultiplier() => _currentMultiplier+=_holdMultiplier;

    public void ClearMultiplier() => _currentMultiplier = 1f;

}
