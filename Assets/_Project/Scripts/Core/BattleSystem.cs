using System.Threading;
using Zenject;
using Cysharp.Threading.Tasks;
using Duels.Units;

namespace Duels.Core
{
    public class BattleSystem
    {
        private UnitFactory _unitFactory;
        private TurnHandler _turnHandler;

        private BattleEvents _battleEvents;

        private BattleState _state;

        private Unit _enemyTeamHero;
        private Unit _playerTeamHero;

        private Unit _firstTurnUnit;
        private Unit _secondTurnUnit;

        private BattleResult _battleResult;

        private const int StartDelay = 500;

        [Inject]
        public void Construct(UnitFactory unitFactory, TurnHandler turnHandler, BattleEvents battleEvents)
        {
            _unitFactory = unitFactory;
            _turnHandler = turnHandler;
            _battleEvents = battleEvents;
        }

        public async UniTask Run(CancellationToken cancellationToken)
        {
            StartBattle();

            await SetUpBattle(cancellationToken);
        }

        private void StartBattle()
        {
            _state = BattleState.Start;

            _battleEvents.RaiseBattleStarted();
        }

        private async UniTask SetUpBattle(CancellationToken cancellationToken)
        {
            _enemyTeamHero = _unitFactory.CreateTeamOneHero();
            _playerTeamHero = _unitFactory.CreateTeamTwoHero();
            
            int turnDecider = UnityEngine.Random.Range(0, 2);

            if (turnDecider == 0)
            {
                _firstTurnUnit = _enemyTeamHero;
                _secondTurnUnit = _playerTeamHero;
            }
            else
            {
                _firstTurnUnit = _playerTeamHero;
                _secondTurnUnit = _enemyTeamHero;
            }

            await UniTask.Delay(StartDelay, cancellationToken: cancellationToken);

            _state = BattleState.EnemyTeamTurn;

            await StartBattleLoop(cancellationToken);
        }

        private async UniTask StartBattleLoop(CancellationToken cancellationToken)
        {
            while (!_battleResult.IsFinished)
            {
                if (_state == BattleState.EnemyTeamTurn)
                {
                    await PerformTurn(_firstTurnUnit, _secondTurnUnit, BattleState.PlayerTeamTurn, cancellationToken);
                }
                else if (_state == BattleState.PlayerTeamTurn)
                {
                    await PerformTurn(_secondTurnUnit, _firstTurnUnit, BattleState.EnemyTeamTurn, cancellationToken);
                }
            }
        }

        private async UniTask PerformTurn(Unit attacker, Unit defender, BattleState nextState, CancellationToken cancellationToken)
        {
            _battleResult = await _turnHandler.HandleTurn(attacker, defender, cancellationToken);

            if (_battleResult.IsFinished)
            {
                return;
            }

            _state = nextState;
        }
    }
}