using Dan.Main;
using UnityEngine;

namespace MainMenu
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        private SceneLoader _sceneLoader;
        private MainMenuMediator _mainMenuMediator;
        private LeaderBoardLoader _leaderBoardLoader;

        private async void Awake() 
        {
            await LeaderboardCreator.WaitForInitialization();
            _sceneLoader = new SceneLoader();
            _leaderBoardLoader = new LeaderBoardLoader();
            await _leaderBoardLoader.Load();

            _mainMenuMediator = new MainMenuMediator(_sceneLoader,_mainMenuUI,_leaderBoardLoader);
        }

        private void OnDestroy() 
        {
            _mainMenuMediator.Dispose();
        }
    }
}
