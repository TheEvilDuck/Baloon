using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopup : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _text;
    [SerializeField]private Button _yestButton;
    [SerializeField]private Button _noButton;

    public event Action confirmed;
    public event Action canceled;

    private void OnEnable() 
    {
        _yestButton.onClick.AddListener(OnYesButtonPressed);
        _noButton.onClick.AddListener(OnNoButtonPressed);
    }

    private void OnDisable() 
    {
        _yestButton.onClick.RemoveListener(OnYesButtonPressed);
        _noButton.onClick.RemoveListener(OnNoButtonPressed);
    }

    public void Show(string text)
    {
        gameObject.SetActive(true);
        _text.text = text;
    }

    private void OnYesButtonPressed()
    {
        confirmed?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnNoButtonPressed()
    {
        canceled?.Invoke();
        gameObject.SetActive(false);
    }
}
