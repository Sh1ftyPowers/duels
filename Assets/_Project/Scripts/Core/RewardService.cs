using System;
using Zenject;
using Duels.Units;

namespace Duels.Core
{
    public class RewardService : IInitializable, IDisposable
    {
        private const int RewardCoins = 100;

        private readonly Wallet _wallet;
        private readonly BattleEvents _battleEvents;

        public RewardService(Wallet wallet, BattleEvents battleEvents)
        {
            _wallet = wallet;
            _battleEvents = battleEvents;
        }

        public void Initialize()
        {
            _battleEvents.WinnerDeclared += OnWinnerDeclared;
        }

        private void OnWinnerDeclared(Unit winner)
        {
            if (winner.TeamType != TeamType.Player)
                return;

            _wallet.AddCoins(RewardCoins);
        }

        public void Dispose()
        {
            _battleEvents.WinnerDeclared -= OnWinnerDeclared;
        }
    }
}