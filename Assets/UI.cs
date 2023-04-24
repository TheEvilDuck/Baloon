using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]Game _game;
    [SerializeField]TextMeshProUGUI _pointsText;
    private void Awake() {
        _game.playerStatsCreated+=(()=>{
            _game.playerStats.pointsChanged+=OnPointsChanged;
        });
    }
    private void OnDisable() {
        _game.playerStats.pointsChanged-=OnPointsChanged;
    }
    private void OnPointsChanged(float points)
    {
        _pointsText.text = "Points: "+points.ToString();
    }
}
