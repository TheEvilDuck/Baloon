using UnityEngine;

namespace MainMenu
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        private SceneLoader _sceneLoader;
        private MainMenuMediator _mainMenuMediator;

        private void Awake() 
        {
            _sceneLoader = new SceneLoader();

            _mainMenuMediator = new MainMenuMediator(_sceneLoader,_mainMenuUI);
        }

        private void OnDestroy() 
        {
            _mainMenuMediator.Dispose();
        }
    }
}
