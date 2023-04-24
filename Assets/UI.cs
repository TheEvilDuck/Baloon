using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]Game _game;
    [SerializeField]TextMeshProUGUI _pointsText;
    private void Awake() {
        _game.playerStatsCreated+=OnPlayerStatsCreated;
    }
    private void OnPlayerStatsCreated()
    {
        _game.playerStats.pointsChanged+=OnPointsChanged;
    }
    private void OnDisable() {
        _game.playerStats.pointsChanged-=OnPointsChanged;
        _game.playerStatsCreated-=OnPlayerStatsCreated;
    }
    private void OnPointsChanged(float points)
    {
        _pointsText.text = "Points: "+Mathf.FloorToInt(points).ToString();
    }
}
