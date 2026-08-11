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

        private Unit _teamOneHero;
        private Unit _teamTwoHero;

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
            _teamOneHero = _unitFactory.CreateTeamOneHero();
            _teamTwoHero = _unitFactory.CreateTeamTwoHero();
            
            int turnDecider = UnityEngine.Random.Range(0, 2);

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

            await UniTask.Delay(StartDelay, cancellationToken: cancellationToken);

            _state = BattleState.TeamOneTurn;

            await StartBattleLoop(cancellationToken);
        }

        private async UniTask StartBattleLoop(CancellationToken cancellationToken)
        {
            while (!_battleResult.IsFinished)
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
            _battleResult = await _turnHandler.HandleTurn(attacker, defender, cancellationToken);

            if (_battleResult.IsFinished)
            {
                return;
            }

            _state = nextState;
        }
    }
}