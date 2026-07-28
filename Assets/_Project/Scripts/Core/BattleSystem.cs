using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Duels.Audio;
using Duels.Presentation;
using Duels.UI;
using Duels.Units;

namespace Duels.Core
{
    public class BattleSystem
    {
        private AudioManager _audioManager;
        private BattlePresenter _battlePresenter;
        private HealthbarPresenter _healthbarPresenter;
        private TurnHandler _turnHandler;
        private UnitSpawner _spawner;

        private BattleState _state;

        private Unit _teamOneHero;
        private Unit _teamTwoHero;

        private Unit _firstTurnUnit;
        private Unit _secondTurnUnit;

        private const int StartDelay = 500;

        public BattleSystem(BattlePresenter battlePresenter, HealthbarPresenter healthbarPresenter, AudioManager audioManager, UnitSpawner spawner, TurnHandler turnHandler)
        {
            _audioManager = audioManager;
            _battlePresenter = battlePresenter;
            _healthbarPresenter = healthbarPresenter;
            _spawner = spawner;
            _turnHandler = turnHandler;
        }

        public async UniTaskVoid Run(CancellationToken cancellationToken)
        {
            try
            {
                _audioManager.PlayBattleMusic();

                _state = BattleState.Start;

                await SetUpBattle(cancellationToken);
            }

            finally
            {
                CleanBattleUp();
            }
        }

        private async UniTask SetUpBattle(CancellationToken cancellationToken)
        {
            int turnDecider = Random.Range(0, 2);

            _teamOneHero = _spawner.SpawnTeamOne();
            _teamTwoHero = _spawner.SpawnTeamTwo();

            _healthbarPresenter.RegisterUnit(_teamOneHero, _teamOneHero.GetComponentInChildren<Healthbar>());
            _healthbarPresenter.RegisterUnit(_teamTwoHero, _teamTwoHero.GetComponentInChildren<Healthbar>());

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

            _battlePresenter.ShowBattleStart();

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

        private void CleanBattleUp()
        {
            _turnHandler?.Dispose();
        }
    }
}