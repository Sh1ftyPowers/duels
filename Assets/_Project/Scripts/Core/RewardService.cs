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
        private readonly BattleContext _battleContext;

        public RewardService(Wallet wallet, BattleEvents battleEvents, BattleContext battleContext)
        {
            _wallet = wallet;
            _battleEvents = battleEvents;
            _battleContext = battleContext;
        }

        public void Initialize()
        {
            _battleEvents.WinnerDeclared += OnWinnerDeclared;
        }

        private void OnWinnerDeclared(Unit winner)
        {
            if (winner != _battleContext.PlayerHero)
                return;

            _wallet.AddCoins(RewardCoins);
        }

        public void Dispose()
        {
            _battleEvents.WinnerDeclared -= OnWinnerDeclared;
        }
    }
}