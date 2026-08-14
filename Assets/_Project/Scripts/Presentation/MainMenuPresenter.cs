using System;
using Zenject;
using Duels.Core;
using Duels.UI;


namespace Duels.Presentation
{
    public class MainMenuPresenter : IInitializable, IDisposable
    {
        private readonly MainMenuView _mainMenuView;
        private readonly Wallet _wallet;
        private readonly BattleView _battleView;
        private readonly BattleEvents _battleEvents;

        public MainMenuPresenter(MainMenuView mainMenuView, Wallet wallet, BattleView battleView, BattleEvents battleEvents)
        {
            _mainMenuView = mainMenuView;
            _wallet = wallet;
            _battleView = battleView;
            _battleEvents = battleEvents;
        }

        public void Initialize()
        {
            _wallet.CoinsChanged += OnCoinsChanged;
            _battleEvents.BattleEnded += OnBattleEnded;

            OnCoinsChanged(_wallet.Coins);
        }

        private void OnCoinsChanged(int coins)
        {
            _mainMenuView.SetCoins(coins);
        }

        private void OnBattleEnded()
        {
            _battleView.HideBattleUI();
            _mainMenuView.ShowMainMenuUI();
        }

        public void Dispose()
        {
            _wallet.CoinsChanged -= OnCoinsChanged;
            _battleEvents.BattleEnded -= OnBattleEnded;
        }
    }
}