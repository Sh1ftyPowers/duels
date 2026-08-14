using System;
using Duels.Units;

namespace Duels.Core
{
    public class BattleEvents
    {
        public event Action BattleStarted;
        public event Action<Unit> WinnerDeclared;
        public event Action BattleEnded;

        public void RaiseBattleStarted()
        {
            BattleStarted?.Invoke();
        }

        public void RaiseWinnerDeclared(Unit winner)
        {
            WinnerDeclared?.Invoke(winner);
        }

        public void RaiseBattleEnded()
        {
            BattleEnded?.Invoke();
        }
    }
}