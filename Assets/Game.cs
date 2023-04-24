using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField]float _maxBaloonSize;
    [SerializeField]float _growStep;
    [SerializeField]float _pointsPerStep;
    [SerializeField]float _holdMultiplier;
    [SerializeField]float _holdTimeToStartBreath;
    private float _currentHoldTime = 0;
    private bool _breath = false;
    public event Action playerStatsCreated;
    public event Action playerBreathStarted;
    public event Action playerBreathEnded;

    public PlayerStats playerStats
    {
        get;
        private set;
    }

    [SerializeField]Baloon _baloonPrefab;

    private void OnEnable() 
    {
        PlayerInput.instance.tapStarted+=OnTapStarted;
        PlayerInput.instance.tapEnded+=OnTapEnded;
    }
    private void OnDisable() 
    {
        PlayerInput.instance.tapStarted-=OnTapStarted;
        PlayerInput.instance.tapEnded-=OnTapEnded;
    }
    private void OnTapStarted()
    {
        _breath = true;
    }
    private void OnTapEnded()
    {
        _breath = false;
        playerBreathEnded?.Invoke();
    }
    void Start()
    {
        Baloon baloon = InitBaloon();
        InitPlayerStats(baloon);
    }

    public Baloon InitBaloon()
    {
        Baloon baloon = Instantiate(_baloonPrefab);
        baloon.SetValues(_growStep,_maxBaloonSize, this);
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
    private void Update() 
    {
        if (_breath)
        {
            if (_currentHoldTime>=_holdTimeToStartBreath)
                playerBreathStarted?.Invoke();
            else
                _currentHoldTime+=Time.deltaTime;
        }else if (_currentHoldTime!=0)
            _currentHoldTime = 0;
    }
}
