using UnityEngine;
using Duels.Audio;
using Duels.UI;
using Duels.Units;
using Duels.Effects;
using Cysharp.Threading.Tasks;

namespace Duels.Core
{
    public class GameCompositionRoot : MonoBehaviour
    {
        [SerializeField] private BattleUI _battleUI;
        [SerializeField] private MessageSystem _messageSystem;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private UnitSpawner _spawner;
        [SerializeField] private GameObject _gameOverCanvas;
        
        private BattleSystem _battleSystem;

        private void Awake()
        {
            Compose();
        }

        public void Compose()
        {
            var effectsManager = new EffectsManager();

            var victoryHandler = new VictoryHandler(_battleUI, _gameOverCanvas, _audioManager);

            var turnHandler = new TurnHandler(_battleUI, effectsManager, victoryHandler, _messageSystem);

            _battleSystem = new BattleSystem();

            _battleSystem.Initialize(_battleUI, _audioManager, _spawner, turnHandler);

            _battleSystem.Run(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}