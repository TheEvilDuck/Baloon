using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]Button _startGameButton;
    [SerializeField]Button _exitGameButton;
    [SerializeField]TextMeshProUGUI _entryTextPrefab;
    [SerializeField]Transform _entriesParent;

    public event Action startButtonPressed;
    public event Action exitButtonPressed;

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
