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
    [SerializeField] Button _reloadScene;
    [SerializeField] Button _doneButton;
    [SerializeField] Button _sumbitButton;
    [SerializeField]Button _exitButton;
    [SerializeField]TextMeshProUGUI _pointsInResultMenu;
    [SerializeField]TMP_InputField _submitName;
    [SerializeField]TextMeshProUGUI _submitResultText;

    string[] _banWords = {
        "Cunt","Fuck","Bitch","Cock","Suck","Dick","Faggot","Nigger"
    };

    private void OnEnable() {
        _game.playerStatsCreated+=OnPlayerStatsCreated;
        _game.playerBreathEnded+=OnPlayerEndBreath;
        _game.inhale+=OnPlayerInhale;
        _game.acceptedCurrentBaloonState+=ShowResultMenu;
        _resultMenu.SetActive(false);
        _breathUI.SetActive(false);
        _doneButton.onClick.AddListener(_game.AcceptCurrentBaloonState);
        _reloadScene.onClick.AddListener(SceneLoader.instance.ReloadScene);
        _sumbitButton.onClick.AddListener(OnSubmitButtonPressed);
        _exitButton.onClick.AddListener(OnExitButtonPressed);

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
        _doneButton.onClick.RemoveListener(_game.AcceptCurrentBaloonState);
        _reloadScene.onClick.RemoveListener(SceneLoader.instance.ReloadScene);
        _sumbitButton.onClick.RemoveListener(OnSubmitButtonPressed);
        _exitButton.onClick.RemoveListener(OnExitButtonPressed);
    }
    public void OnInpitFieldFocusEnd()
    {
        
    }
    private void OnExitButtonPressed()
    {
        // 1 is for main menu
        SceneLoader.instance.LoadScene(1);
    }
    private void OnSubmitButtonPressed()
    {
        _submitResultText.gameObject.SetActive(true);
        if (_submitName.text==string.Empty)
        {
            _submitResultText.text = "No name?";
            _submitResultText.color = Color.red;
            return;
        }
        if (IsNameHasBanWords(_submitName.text))
        {
            _submitResultText.text = "This name is now allowed!";
            _submitResultText.color = Color.red;
            _submitName.text = string.Empty;
            return;
        }
        if (_submitName.text.Length<3)
        {
            _submitResultText.text = "The name is too short!";
            _submitResultText.color = Color.red;
            return;
        }
        _submitResultText.text = "Success!";
        _submitResultText.color = Color.green;
        _game.SumbitCurrentScore(_submitName.text);
    }
    private bool IsNameHasBanWords(string name)
    {
        foreach (string banWord in _banWords)
        {
            if (name.ToLower().Contains(banWord.ToLower()))
                return true;
        }
        return false;
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
        _pointsInResultMenu.text = $"Your score: {Mathf.FloorToInt(_game.playerStats.points)}";
    }

}
