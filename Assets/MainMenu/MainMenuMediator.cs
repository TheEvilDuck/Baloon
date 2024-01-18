using System;
using UnityEngine;

public class MainMenuMediator: IDisposable
{
    private SceneLoader _sceneLoader;
    private MainMenuUI _mainMenuUI;

    public MainMenuMediator(SceneLoader sceneLoader, MainMenuUI mainMenuUI)
    {
        _sceneLoader = sceneLoader;
        _mainMenuUI = mainMenuUI;

        _mainMenuUI.startButtonPressed+=OnStartButtonPressed;
        _mainMenuUI.exitButtonPressed+=OnExitButtonPressed;
    }

    public void Dispose()
    {
        _mainMenuUI.startButtonPressed-=OnStartButtonPressed;
        _mainMenuUI.exitButtonPressed-=OnExitButtonPressed;
    }

    private void OnStartButtonPressed() => _sceneLoader.LoadGameplay();

    private void OnExitButtonPressed() => Application.Quit();
}
