using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]Button _startGameButton;
    [SerializeField]Button _exitGameButton;
    [SerializeField]Button _leaderBoardButton;
    [SerializeField]Button _backToMainMenuButton;

    private void OnEnable() {
        _startGameButton.onClick.AddListener(StartGame);
    }
    private void OnDisable() {
        _startGameButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        SceneLoader.instance.LoadScene(0);
    }
}
