using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField]float _maxBaloonSize;
    [SerializeField]float _minSizePercent;
    [SerializeField]float _maxSizePercent;
    [SerializeField]float _growStep;
    [SerializeField]float _pointsPerStep;
    [SerializeField]float _holdMultiplier;
    [SerializeField]float _holdTimeToStartBreath;
    private float _currentHoldTime = 0;
    private bool _inhale = false;
    private bool _breath = false;
    public Baloon baloon 
    {
        get;
        private set;
    }
    public event Action playerStatsCreated;
    public event Action baloonCreated;
    public event Action playerBreathStarted;
    public event Action playerBreathEnded;
    public event Action<float> inhale;
    public event Action inhaleStarted;
    public event Action acceptedCurrentBaloonState;

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
        _inhale = true;
        inhaleStarted?.Invoke();
    }
    private void OnTapEnded()
    {
        _inhale = false;
        _breath = false;
        playerBreathEnded?.Invoke();
    }
    void Start()
    {
        float sizePercent = UnityEngine.Random.Range(_minSizePercent,_maxSizePercent);
        baloon = InitBaloon(sizePercent);
        InitPlayerStats();
    }

    public Baloon InitBaloon(float sizePercent)
    {
        baloon = Instantiate(_baloonPrefab);
        baloon.SetValues(_growStep,_maxBaloonSize*sizePercent, this);
        Color randomColor = new Color(
            UnityEngine.Random.Range(0,1f),
            UnityEngine.Random.Range(0,1f),
            UnityEngine.Random.Range(0,1f)
        );
        baloon.UpdateBaloonColor(randomColor);
        baloonCreated?.Invoke();
        baloon.exploded+=(()=>{
            playerStats.OnBaloonExploded();
            AcceptCurrentBaloonState();
        });
        return baloon;
    }
    public void AcceptCurrentBaloonState()
    {
        PlayerInput.instance.tapStarted-=OnTapStarted;
        PlayerInput.instance.tapEnded-=OnTapEnded;
        _inhale = false;
        _breath = false;
        playerBreathEnded?.Invoke();
        acceptedCurrentBaloonState?.Invoke();
    }
    public void InitPlayerStats()
    {
        playerStats = new PlayerStats(_pointsPerStep,_holdMultiplier);
        baloon.grown+=playerStats.OnBaloonGrow;
        baloon.exploded+=playerStats.OnBaloonExploded;
        PlayerInput.instance.tapEnded+=playerStats.OnGrowStop;
        playerStatsCreated?.Invoke();
    }
    private void Update() 
    {
        if (_inhale)
        {
            inhale?.Invoke(_currentHoldTime/_holdTimeToStartBreath);
            if (_currentHoldTime>=_holdTimeToStartBreath)
            {
                if (!_breath)
                {
                    _breath = true;
                    playerBreathStarted?.Invoke();
                }
            }
            else
                _currentHoldTime+=Time.deltaTime;
        }else if (_currentHoldTime!=0)
            _currentHoldTime = 0;
    }
}
