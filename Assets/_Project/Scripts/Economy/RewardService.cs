using System;
using Zenject;
using Duels.Core;
using Duels.Units;

namespace Duels.Economy
{
    public class RewardService : IInitializable, IDisposable
    {
        private readonly Wallet _wallet;
        private readonly WalletConfig _walletConfig;
        private readonly BattleEvents _battleEvents;
        private readonly BattleContext _battleContext;
        private readonly PlayerProgressEvents _playerProgressEvents;

        public RewardService(Wallet wallet, WalletConfig walletConfig, BattleEvents battleEvents, BattleContext battleContext, PlayerProgressEvents playerProgressEvents)
        {
            _wallet = wallet;
            _walletConfig = walletConfig;
            _battleEvents = battleEvents;
            _battleContext = battleContext;
            _playerProgressEvents = playerProgressEvents;
        }

        public void Initialize()
        {
            _battleEvents.WinnerDeclared += OnWinnerDeclared;
        }

        private void OnWinnerDeclared(Unit winner)
        {
            if (winner != _battleContext.PlayerHero)
                return;

            _wallet.AddCoins(_walletConfig.VictoryReward);
            _playerProgressEvents.RaiseProgressChanged();

        }

        public void Dispose()
        {
            _battleEvents.WinnerDeclared -= OnWinnerDeclared;
        }
    }
}