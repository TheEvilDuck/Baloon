using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private TextMeshProUGUI _entryTextPrefab;
    [SerializeField] private Transform _entriesParent;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Button _cancelLoadingButton;

    public event Action startButtonPressed;
    public event Action exitButtonPressed;
    public event Action cancelLoadingButtonPressed;

    public void LoadLeaderboardText(IEnumerable<PlayerData> entries)
    {

        if (entries==null)
            return;

        foreach (PlayerData playerData in entries)
        {
            TextMeshProUGUI entryText = Instantiate(_entryTextPrefab,_entriesParent);
            entryText.text = $"{playerData.Name}: {playerData.Score}";
        }
    }
    public void HideLoadingScreen() => _loadingScreen.SetActive(false);
    public void SpawnMessage(string message)
    {
        TextMeshProUGUI messageObject = Instantiate(_entryTextPrefab,_entriesParent);
        messageObject.text = message;
    }
    private void OnEnable() {
        _startGameButton.onClick.AddListener(OnStartPressed);
        _exitGameButton.onClick.AddListener(OnExitPressed);
        _cancelLoadingButton.onClick.AddListener(OnCancelLoadingPressed);
    }
    private void OnDisable() {
        _startGameButton.onClick.RemoveListener(OnStartPressed);
        _exitGameButton.onClick.RemoveListener(OnExitPressed);
        _cancelLoadingButton.onClick.RemoveListener(OnCancelLoadingPressed);
    }

    private void OnStartPressed() => startButtonPressed?.Invoke();
    private void OnExitPressed() => exitButtonPressed?.Invoke();
    private void OnCancelLoadingPressed() => cancelLoadingButtonPressed?.Invoke();
}
