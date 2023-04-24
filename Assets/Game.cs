using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]float _maxBaloonSize;
    [SerializeField]float _growStep;
    [SerializeField]float _pointsPerStep;
    [SerializeField]float _holdMultiplier;

    private PlayerStats _playerStats;

    [SerializeField]Baloon _baloonPrefab;
    void Start()
    {
        Baloon baloon = InitBaloon();
        InitPlayerStats(baloon);
        _playerStats.pointsChanged+=((float points)=>{
            Debug.Log(points);
        });
    }

    public Baloon InitBaloon()
    {
        Baloon baloon = Instantiate(_baloonPrefab);
        baloon.SetValues(_growStep,_maxBaloonSize);
        return baloon;
    }
    public void InitPlayerStats(Baloon baloon)
    {
        _playerStats = new PlayerStats(_pointsPerStep,_holdMultiplier);
        baloon.grown+=_playerStats.OnBaloonGrow;
        baloon.exploded+=_playerStats.OnBaloonExploded;
        PlayerInput.instance.tapEnded+=_playerStats.OnGrowStop;
    }
}
