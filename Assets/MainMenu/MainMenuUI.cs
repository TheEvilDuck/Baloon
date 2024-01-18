using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]Button _startGameButton;
    [SerializeField]Button _exitGameButton;

    public event Action startButtonPressed;
    public event Action exitButtonPressed;
    private void OnEnable() {
        _startGameButton.onClick.AddListener(OnStartPressed);
        _exitGameButton.onClick.AddListener(OnExitPressed);
    }
    private void OnDisable() {
        _startGameButton.onClick.RemoveListener(OnStartPressed);
        _exitGameButton.onClick.RemoveListener(OnExitPressed);
    }

    private void OnStartPressed() => startButtonPressed?.Invoke();
    private void OnExitPressed() => exitButtonPressed?.Invoke();
}
