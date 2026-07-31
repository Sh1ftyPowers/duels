using System;
using Duels.Units;

namespace Duels.Core
{
    public class BattleEvents
    {
        public event Action BattleStarted;
        public event Action<Unit, Unit> WinnerDelcared;
        public event Action BattleEnded;

        public void RaiseBattleStarted()
        {
            BattleStarted?.Invoke();
        }

        public void RaiseWinnerDelcared(Unit winner, Unit loser)
        {
            WinnerDelcared?.Invoke(winner, loser);
        }

        public void RaiseBattleEnded()
        {
            BattleEnded?.Invoke();
        }
    }
}