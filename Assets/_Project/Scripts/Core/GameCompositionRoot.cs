using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Duels.Audio;
using Duels.UI;
using Duels.Units;
using Duels.Effects;
using System.Runtime.CompilerServices;

namespace Duels.Core
{
    public class GameCompositionRoot : MonoBehaviour
    {
        [SerializeField] private BattleUI _battleUI;
        [SerializeField] private UnitSpawner _spawner;
        [SerializeField] private GameObject _gameOverCanvas;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioClip _battleTheme;
        [SerializeField] private AudioClip _victorySound;
        [SerializeField] private AudioClip _restartMenuTheme;

        [SerializeField] private Button _restartButton;

        [SerializeField] private GameObject[] _teamOnePrefabs;
        [SerializeField] private GameObject[] _teamTwoPrefabs;

        [SerializeField] private Transform _teamOneSpawnPoint;
        [SerializeField] private Transform _teamTwoSpawnPoint;

        private AudioManager _audioManager;
        private BattleSystem _battleSystem;
        private EffectsManager _effectsManager;
        private GameRestarter _gameRestarter;
        private MessageSystem _messageSystem;
        private TurnHandler _turnHandler;
        private UnitSpawner _unitSpawner;
        private VictoryHandler _victoryHandler;

        private CancellationToken _token;

        private void Awake()
        {
            Compose();
        }

        public void Compose()
        {
            _token = this.GetCancellationTokenOnDestroy();

            _audioManager = new AudioManager(_musicSource, _battleTheme, _victorySound, _restartMenuTheme);

            _messageSystem = new MessageSystem(_battleUI, _token);

            _gameRestarter = new GameRestarter(_restartButton);

            _effectsManager = new EffectsManager();

            _unitSpawner = new UnitSpawner(_teamOnePrefabs, _teamTwoPrefabs, _teamOneSpawnPoint, _teamTwoSpawnPoint);

            _victoryHandler = new VictoryHandler(_battleUI, _gameOverCanvas, _audioManager);

            _turnHandler = new TurnHandler(_battleUI, _effectsManager, _victoryHandler, _messageSystem);

            _battleSystem = new BattleSystem();

            _battleSystem.Initialize(_battleUI, _audioManager, _unitSpawner, _turnHandler);

            _battleSystem.Run(_token).Forget();
        }

        private void OnDestroy()
        {
            _gameRestarter?.Dispose();
        }
    }
}