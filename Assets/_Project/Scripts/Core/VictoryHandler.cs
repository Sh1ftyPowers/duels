using Duels.Units;

namespace Duels.Core
{
    public class VictoryHandler
    {
        private readonly BattleEvents _battleEvents;

        public VictoryHandler(BattleEvents battleEvents)
        {
            _battleEvents = battleEvents;
        }

        public bool IsDead(Unit unit)
        {
            return unit.CurrentHealthPoints == 0;
        }

        public BattleResult HandleVictory(Unit winner, Unit loser)
        {
            winner.PlayVictoryAnimation();
            loser.PlayDeathAnimation();

            var results = new BattleResult(winner, loser);

            _battleEvents.RaiseWinnerDeclared(winner);
            _battleEvents.RaiseBattleEnded();

            return results;
        }
    }
}