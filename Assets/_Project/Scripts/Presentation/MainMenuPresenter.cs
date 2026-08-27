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
        private readonly BattleEvents _battleEvents;
        private readonly BattleStarter _battleStarter;
        private readonly GameExiter _gameExiter;

        public MainMenuPresenter(MainMenuView mainMenuView, Wallet wallet, BattleEvents battleEvents, BattleStarter battleStarter, GameExiter gameExiter)
        {
            _mainMenuView = mainMenuView;
            _wallet = wallet;
            _battleEvents = battleEvents;
            _battleStarter = battleStarter;
            _gameExiter = gameExiter;
        }

        public void Initialize()
        {
            _mainMenuView.StartGameButton.onClick.AddListener(StartGame);

            _mainMenuView.ExitGameButton.onClick.AddListener(_gameExiter.ExitGame);

            _wallet.CoinsChanged += OnCoinsChanged;

            _battleEvents.BattleEnded += OnBattleEnded;

            OnCoinsChanged(_wallet.Coins);
        }

        private void StartGame()
        {
            _mainMenuView.HideMainMenuUI();

            _battleStarter.Start();
        }

        private void OnCoinsChanged(int coins)
        {
            _mainMenuView.SetCoins(coins);
        }

        private void OnBattleEnded()
        {
            _mainMenuView.ShowMainMenuUI();
        }

        public void Dispose()
        {
            _mainMenuView.StartGameButton.onClick.RemoveListener(StartGame);

            _mainMenuView.ExitGameButton.onClick.RemoveListener(_gameExiter.ExitGame);

            _wallet.CoinsChanged -= OnCoinsChanged;

            _battleEvents.BattleEnded -= OnBattleEnded;
        }
    }
}