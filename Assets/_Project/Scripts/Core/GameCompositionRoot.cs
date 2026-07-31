using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Duels.Audio;
using Duels.UI;
using Duels.Units;
using Duels.Effects;
using Duels.Presentation;

namespace Duels.Core
{
    public class GameCompositionRoot : MonoBehaviour
    {
        [SerializeField] private BattleView _battleView;

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
        private BattlePresenter _battlePresenter;
        private BattleSystem _battleSystem;
        private EffectsManager _effectsManager;
        private GameRestarter _gameRestarter;
        private HealthbarPresenter _healthbarPresenter;
        private MessageSystem _messageSystem;
        private TurnHandler _turnHandler;
        private UnitFactory _unitFactory;
        private UnitSpawner _unitSpawner;
        private VictoryHandler _victoryHandler;

        private CancellationToken _token;

        private void Awake()
        {
            Compose();

            _battleSystem.Run(_token).Forget();
        }

        private void Compose()
        {
            _token = this.GetCancellationTokenOnDestroy();

            _audioManager = new AudioManager(_musicSource, _battleTheme, _victorySound, _restartMenuTheme);

            _messageSystem = new MessageSystem(_token);

            _healthbarPresenter = new HealthbarPresenter();

            _battlePresenter = new BattlePresenter(_battleView, _messageSystem);

            _gameRestarter = new GameRestarter(_restartButton);

            _effectsManager = new EffectsManager();

            _unitSpawner = new UnitSpawner(_teamOnePrefabs, _teamTwoPrefabs, _teamOneSpawnPoint, _teamTwoSpawnPoint);

            _unitFactory = new UnitFactory(_unitSpawner, _healthbarPresenter);

            _victoryHandler = new VictoryHandler(_battlePresenter, _audioManager);

            _turnHandler = new TurnHandler(_effectsManager, _victoryHandler, _battlePresenter);

            _battleSystem = new BattleSystem(_battlePresenter, _unitFactory, _audioManager, _turnHandler);
        }

        private void OnDestroy()
        {
            _gameRestarter?.Dispose();
            _battlePresenter?.Dispose();
            _healthbarPresenter?.Dispose();
        }
    }
}