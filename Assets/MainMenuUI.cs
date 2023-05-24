using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]Button _startGameButton;
    [SerializeField]Button _exitGameButton;
    private void OnEnable() {
        _startGameButton.onClick.AddListener(StartGame);
        _exitGameButton.onClick.AddListener(ExitGame);
    }
    private void OnDisable() {
        _startGameButton.onClick.RemoveListener(StartGame);
        _exitGameButton.onClick.RemoveListener(ExitGame);
    }

    private void StartGame()
    {
        SceneLoader.instance.LoadScene(0);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
