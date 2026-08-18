using Duels.Units;

namespace Duels.Core
{
    public readonly struct BattleResult
    {
        public bool IsFinished { get; }
        public Unit Winner { get; }
        public Unit Loser { get; }

        public BattleResult(Unit winner, Unit loser)
        {
            IsFinished = true;
            Winner = winner;
            Loser = loser;
        }
    }
}