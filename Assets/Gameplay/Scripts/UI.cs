using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;

namespace Gameplay
{
    public class UI : MonoBehaviour
    {
        private const string CONFIRM_POPUP_TEXT = "Are you sure you want to update your name?";

        [SerializeField] private TextMeshProUGUI _pointsText;
        [SerializeField] private GameObject _breathUI;
        [SerializeField] private Slider _breathBarSlider;
        [SerializeField] private GameObject _resultMenu;
        [SerializeField] private  Button _reloadSceneButton;
        [SerializeField] private  Button _doneButton;
        [SerializeField] private  Button _sumbitButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TextMeshProUGUI _pointsInResultMenu;
        [SerializeField] private TMP_InputField _submitName;
        [SerializeField] private TextMeshProUGUI _submitResultText;
        [SerializeField] private ConfirmPopup _confirmPopup;

        public event Action reloadButtonPressed;
        public event Action enoughButtonPressed;
        public event Func<Task<bool>> submitButtonPressed;
        public event Action exitButtonPressed;

        private readonly string[] _banWords = {
            "Cunt","Fuck","Bitch","Cock","Suck","Dick","Faggot","Nigger"
        };

        private bool _inhale = false;
        private float _inhaleTime;
        private string _nameBuffer;

        public string EnteredPlayerName => _submitName.text;

        private void OnEnable() {
            _resultMenu.SetActive(false);
            _breathUI.SetActive(false);
            _sumbitButton.onClick.AddListener(OnSubmitButtonPressed);
            _exitButton.onClick.AddListener(OnExitButtonPressed);
            _reloadSceneButton.onClick.AddListener(OnReloadButtonPressed);
            _doneButton.onClick.AddListener(OnEnoughButtonPressed);

            _confirmPopup.confirmed+=OnConfirmChangeNamePressed;
            _confirmPopup.canceled+=OnCancelChangeNamePressed;

        }
        private void OnDisable() {
            _sumbitButton.onClick.RemoveListener(OnSubmitButtonPressed);
            _exitButton.onClick.RemoveListener(OnExitButtonPressed);
            _reloadSceneButton.onClick.RemoveListener(OnReloadButtonPressed);
            _doneButton.onClick.RemoveListener(OnEnoughButtonPressed);

            _confirmPopup.confirmed-=OnConfirmChangeNamePressed;
            _confirmPopup.canceled-=OnCancelChangeNamePressed;
        }

        private void Update() 
        {
            if (!_inhale)
                return;

            _breathBarSlider.value+=1f/(_inhaleTime/Time.deltaTime);
        }

        public void Init(float inhaleTime)
        {
            _inhaleTime = inhaleTime;
        }
       
        public void UpdatePointsText(int points)
        {
            _pointsText.text = $"Points: {points}";;
            _pointsInResultMenu.text = $"Your finale score: {points}";
        }
        public void UpdateInhaleBar(float holdTimePercent) => _breathBarSlider.value = holdTimePercent;
        public void UpdateName(string name)
        {
            _submitName.text = name;
            _nameBuffer = name;
        }
        public void ShowInhaleBar()
        {
            _breathUI.SetActive(true);
            _inhale = true;
            _breathBarSlider.value = 0;
        }
        public void HideInhaleBar()
        {
            _breathUI.SetActive(false);
            _inhale = false;
        }
        public void ShowResultMenu()
        {
            _resultMenu.SetActive(true);
            HideInhaleBar();
            HidePointsText();
        }

        public void OnReloadButtonPressed() => reloadButtonPressed?.Invoke();
        public void HideSubmit()
        {
            _submitName.gameObject.SetActive(false);
            _sumbitButton.gameObject.SetActive(false);
            _pointsInResultMenu.text = "You lost";
        }

        private void HidePointsText() =>  _pointsText.gameObject.SetActive(false);

        private void OnExitButtonPressed() => exitButtonPressed?.Invoke();
        private async void OnSubmitButtonPressed()
        {
            _submitResultText.gameObject.SetActive(true);
            _submitResultText.color = Color.red;
            _submitResultText.text = "Waiting for response...";

            if (_submitName.text==string.Empty)
            {
                _submitResultText.text = "No name?";
                return;
            }
            if (DoesNameHasBanWords(_submitName.text))
            {
                _submitResultText.text = "This name is now allowed!";
                _submitName.text = string.Empty;
                return;
            }
            if (_submitName.text.Length<3)
            {
                _submitResultText.text = "The name is too short!";
                return;
            }

            if (_nameBuffer!=string.Empty)
            {
                
                if (string.Compare(_nameBuffer,_submitName.text)!=0)
                {
                    _confirmPopup.Show(CONFIRM_POPUP_TEXT);
                    return;
                }
            }

            bool? success = await submitButtonPressed?.Invoke();

            if (success==null)
            {
                _submitResultText.text = "Something went wrong...";
                return;
            }

            if ((bool)success==false)
            {
                _submitResultText.text = "Error while sending info";
                return;
            }

            _submitResultText.text = "Success!";
            _submitResultText.color = Color.green;

        }

        private void OnEnoughButtonPressed()
        {
            ShowResultMenu();
            enoughButtonPressed?.Invoke();
        }

        private void OnConfirmChangeNamePressed()
        {
            _nameBuffer = _submitName.text;
            OnSubmitButtonPressed();
        }

        private void OnCancelChangeNamePressed()
        {
            _submitName.text = _nameBuffer;
            _submitResultText.gameObject.SetActive(false);
        }
        private bool DoesNameHasBanWords(string name)
        {
            foreach (string banWord in _banWords)
            {
                if (name.ToLower().Contains(banWord.ToLower()))
                    return true;
            }
            return false;
        }

    }
}
