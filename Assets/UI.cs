using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]Game _game;
    [SerializeField]TextMeshProUGUI _pointsText;
    [SerializeField]GameObject _breathUI;
    [SerializeField]Slider _breathBarSlider;
    [SerializeField]GameObject _resultMenu;
    public Button reloadScene;
    [SerializeField] Button doneButton;
    [SerializeField]TextMeshProUGUI _pointsInResultMenu;

    private void OnEnable() {
        _game.playerStatsCreated+=OnPlayerStatsCreated;
        _game.playerBreathEnded+=OnPlayerEndBreath;
        _game.inhale+=OnPlayerInhale;
        _game.acceptedCurrentBaloonState+=ShowResultMenu;
        _resultMenu.SetActive(false);
        _breathUI.SetActive(false);
        doneButton.onClick.AddListener(_game.AcceptCurrentBaloonState);
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
        _game.acceptedCurrentBaloonState-=ShowResultMenu;
        doneButton.onClick.RemoveListener(_game.AcceptCurrentBaloonState);
    }
    private void OnPointsChanged(float points)
    {
        _pointsText.text = "Points: "+Mathf.FloorToInt(points).ToString();
    }
    private void OnPlayerEndBreath()
    {
        _breathUI.SetActive(false);
    }
    private void OnPlayerInhale(float holdTimePercent)
    {
        _breathUI.SetActive(true);
        _breathBarSlider.value = holdTimePercent;
    }
    private void ShowResultMenu()
    {
        _resultMenu.SetActive(true);
        _breathUI.SetActive(false);
        _pointsText.gameObject.SetActive(false);
        _pointsInResultMenu.text = $"Your score: {_game.playerStats.points}";
    }

}
