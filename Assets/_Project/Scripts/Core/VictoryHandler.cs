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

        public void HandleVictory(Unit winner, Unit loser)
        {
            winner.PlayVictoryAnimation();
            loser.PlayDeathAnimation();

            _battleEvents.RaiseWinnerDeclared(winner, loser);
            _battleEvents.RaiseBattleEnded();
        }
    }
}