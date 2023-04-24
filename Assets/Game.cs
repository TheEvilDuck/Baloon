using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField]float _maxBaloonSize;
    [SerializeField]float _growStep;
    [SerializeField]float _pointsPerStep;
    [SerializeField]float _holdMultiplier;
    public event Action playerStatsCreated;

    public PlayerStats playerStats
    {
        get;
        private set;
    }

    [SerializeField]Baloon _baloonPrefab;
    void Start()
    {
        Baloon baloon = InitBaloon();
        InitPlayerStats(baloon);
    }

    public Baloon InitBaloon()
    {
        Baloon baloon = Instantiate(_baloonPrefab);
        baloon.SetValues(_growStep,_maxBaloonSize);
        return baloon;
    }
    public void InitPlayerStats(Baloon baloon)
    {
        playerStats = new PlayerStats(_pointsPerStep,_holdMultiplier);
        baloon.grown+=playerStats.OnBaloonGrow;
        baloon.exploded+=playerStats.OnBaloonExploded;
        PlayerInput.instance.tapEnded+=playerStats.OnGrowStop;
        playerStatsCreated?.Invoke();
    }
}
