using UnityEngine;
using LeaderBoard;
using System;
using Containers;

namespace MainMenu
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private PeristantContainer _containerPrefab;
        private SceneLoader _sceneLoader;
        private MainMenuMediator _mainMenuMediator;
        private LeaderBoardLoader _leaderBoardLoader;

        private void Awake() 
        {
            PeristantContainer container = FindObjectOfType<PeristantContainer>();

            if (container==null)
                container = Instantiate(_containerPrefab);

            if (!container.Initilizied)
            {
                container.Init();
                _leaderBoardLoader = new LeaderBoardLoader();

                if (!container.TryRegister(_leaderBoardLoader))
                    throw new Exception("Can't register leaderboard");
            }
            else
            {
                if (!container.TryGet<LeaderBoardLoader>(out object tempObj))
                    throw new Exception("Can't load leaderboard");

                _leaderBoardLoader = (LeaderBoardLoader)tempObj;
            }
            
            _sceneLoader = new SceneLoader();

            _mainMenuMediator = new MainMenuMediator(_sceneLoader,_mainMenuUI,_leaderBoardLoader);

            _leaderBoardLoader.Load();
        }

        private void OnDestroy() 
        {
            _mainMenuMediator.Dispose();
        }
    }
}
