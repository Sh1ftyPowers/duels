using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Duels.Units;
using Duels.UI;
using Duels.Audio;

namespace Duels.Core
{
    public class BattleSystem
    {
        private AudioManager _audioManager;
        private BattleUI _battleUI;
        private TurnHandler _turnHandler;
        private UnitSpawner _spawner;

        private BattleState _state;

        private Unit _teamOneHero;
        private Unit _teamTwoHero;

        private Unit _firstTurnUnit;
        private Unit _secondTurnUnit;

        private const int StartDelay = 500;

        public async UniTaskVoid Run(CancellationToken cancellationToken)
        {
            _audioManager.PlayBattleMusic();

            _state = BattleState.Start;
            
            await SetUpBattle(cancellationToken);
        }

        public void Initialize(BattleUI battleUI, AudioManager audioManager, UnitSpawner spawner, TurnHandler turnHandler)
        {
            _audioManager = audioManager;
            _battleUI = battleUI;
            _spawner = spawner;
            _turnHandler = turnHandler;
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

            CleanBattleUp();
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

        private void CleanBattleUp()
        {
            _turnHandler?.Dispose();
        }
    }
}