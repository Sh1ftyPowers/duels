using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Duels.Units;
using Duels.UI;
using Duels.Effects;
using Duels.Audio;

namespace Duels.Core
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverCanvas;
        [SerializeField] private BattleUI _battleUI;
        [SerializeField] private MessageSystem _message;
        [SerializeField] private UnitSpawner _spawner;
        [SerializeField] private AudioManager _audio;

        private BattleState _state;

        private TurnHandler _turnHandler;

        private EffectsManager _effects;

        private VictoryHandler _victoryHandler;

        private Unit _teamOneHero;
        private Unit _teamTwoHero;

        private Unit _firstTurnUnit;
        private Unit _secondTurnUnit;

        private const int StartDelay = 500;

        private async UniTaskVoid Start()
        {
            _audio.PlayBattleMusic();

            _effects = new EffectsManager(_message);

            _victoryHandler = new VictoryHandler(_battleUI, _gameOverCanvas, _audio);

            _turnHandler = new TurnHandler(_battleUI, _message, _effects, _victoryHandler);

            _state = BattleState.Start;
            
            await SetUpBattle(this.GetCancellationTokenOnDestroy());
        }

        private async UniTask SetUpBattle(CancellationToken cancellationToken)
        {
            int turnDecider = Random.Range(0, 2);

            _teamOneHero = _spawner.SpawnTeamOne();
            _teamTwoHero = _spawner.SpawnTeamTwo();

            if (turnDecider == 0)
            {
                _firstTurnUnit = _teamOneHero;
                _secondTurnUnit = _teamTwoHero;
            }
            else
            {
                _firstTurnUnit = _teamTwoHero;
                _secondTurnUnit = _teamOneHero;
            }

            _battleUI.SetTurnText("The Battle Begins!");

            await UniTask.Delay(StartDelay, cancellationToken: cancellationToken);

            _state = BattleState.TeamOneTurn;

            await StartBattleLoop(cancellationToken);
        }

        private async UniTask StartBattleLoop(CancellationToken cancellationToken)
        {
            while (!IsBattleOver())
            {
                if (_state == BattleState.TeamOneTurn)
                {
                    await PerformTurn(_firstTurnUnit, _secondTurnUnit, BattleState.TeamTwoTurn, cancellationToken);
                }
                else if (_state == BattleState.TeamTwoTurn)
                {
                    await PerformTurn(_secondTurnUnit, _firstTurnUnit, BattleState.TeamOneTurn, cancellationToken);
                }
            }
        }

        private async UniTask PerformTurn(Unit attacker, Unit defender, BattleState nextState, CancellationToken cancellationToken)
        {
            bool battleEnded = await _turnHandler.HandleTurn(attacker, defender, cancellationToken);

            if (battleEnded)
            {
                _state = attacker == _teamOneHero
                    ? BattleState.TeamOneVictory
                    : BattleState.TeamTwoVictory;

                return;
            }

            _state = nextState;
        }
        
        private bool IsBattleOver()
        {
            return _state == BattleState.TeamOneVictory || _state == BattleState.TeamTwoVictory;
        }
    }
}