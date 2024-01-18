using UnityEngine;

namespace Gameplay
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField]private BaloonConfig _baloonConfig;
        [SerializeField]private SoundManager _soundManager;
        [SerializeField]private UI _uI;

        private PlayerInput _playerInput;
        private BaloonFactory _baloonFactory;
        private BaloonSpawner _baloonSpawner;
        private GameplayMediator _gameplayMediator;
        private UIMediator _uIMediator;
        private SoundMediator _soundMediator;
        private PlayerStats _playerStats;
        private PlayerStatsCalculator _playerStatsCalculator;
        private BreathController _breathController;
        private SceneLoader _sceneLoader;
        private LeaderBoardLoader _leaderBoardLoader;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _breathController = new BreathController(this, _baloonConfig.HoldTimeToStartBreath);
            _sceneLoader = new SceneLoader();
            _leaderBoardLoader = new LeaderBoardLoader();

            _baloonFactory = new BaloonFactory(_baloonConfig);
            _baloonSpawner = new BaloonSpawner(_baloonFactory);

            _playerStats = new PlayerStats();
            _playerStatsCalculator = new PlayerStatsCalculator(_playerStats,_baloonConfig);

            _gameplayMediator = new GameplayMediator(_baloonSpawner,_playerStatsCalculator,_playerInput,_breathController);
            _uIMediator = new UIMediator(_uI,_sceneLoader,_playerInput,_playerStats,_breathController, _baloonSpawner,_leaderBoardLoader);
            _soundMediator = new SoundMediator(_soundManager,_breathController,_playerInput,_baloonSpawner);

            _uI.Init(_baloonConfig.HoldTimeToStartBreath);
        }

        private void Start() 
        {
            _baloonSpawner.SpawnBaloon();
        }

        private void OnDestroy() 
        {
            _gameplayMediator.Dispose();
            _uIMediator.Dispose();
            _soundMediator.Dispose();
        }

        private void Update() 
        {
            _playerInput.Update();
        }
    }
}
