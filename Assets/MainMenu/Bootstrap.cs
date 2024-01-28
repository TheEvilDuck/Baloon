using UnityEngine;
using LeaderBoard;
using System;
using Containers;

namespace MainMenu
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private PeristantContainer _container;
        private SceneLoader _sceneLoader;
        private MainMenuMediator _mainMenuMediator;
        private LeaderBoardLoader _leaderBoardLoader;

        private async void Awake() 
        {
            _container.Init();
            
            _sceneLoader = new SceneLoader();
            _leaderBoardLoader = new LeaderBoardLoader();
            
            if (!_container.TryRegister(_leaderBoardLoader))
                throw new Exception("Can't register leaderboard");

            await _leaderBoardLoader.Load();

            _mainMenuMediator = new MainMenuMediator(_sceneLoader,_mainMenuUI,_leaderBoardLoader);
        }

        private void OnDestroy() 
        {
            _mainMenuMediator.Dispose();
        }
    }
}
