using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]Game _game;
    [SerializeField]TextMeshProUGUI _pointsText;
    [SerializeField]GameObject _breathUI;
    [SerializeField]RectTransform _breathBar; 
    private bool _inhale = false;
    private void Awake() {
        _game.playerStatsCreated+=OnPlayerStatsCreated;
        _game.playerBreathEnded+=OnPlayerEndBreath;
        _game.inhale+=OnPlayerInhale;
    }
    private void OnPlayerStatsCreated()
    {
        _game.playerStats.pointsChanged+=OnPointsChanged;
    }
    private void OnDisable() {
        _game.playerStats.pointsChanged-=OnPointsChanged;
        _game.playerStatsCreated-=OnPlayerStatsCreated;
        _game.playerBreathEnded-=OnPlayerEndBreath;
        _game.inhale-=OnPlayerInhale;
    }
    private void OnPointsChanged(float points)
    {
        _pointsText.text = "Points: "+Mathf.FloorToInt(points).ToString();
    }
    private void OnPlayerEndBreath()
    {
        _breathUI.SetActive(false);
        _inhale = false;
    }
    private void OnPlayerInhale(float holdTimePercent)
    {
        _breathUI.SetActive(true);
        _breathBar.localPosition = new Vector3(0,(holdTimePercent-1f)*100f,0);
    }

}
