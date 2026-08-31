using System;
using Zenject;
using Duels.Core;
using Duels.UI;
using Duels.Economy;


namespace Duels.Presentation
{
    public class MainMenuPresenter : IInitializable, IDisposable
    {
        private readonly MainMenuView _mainMenuView;
        private readonly Wallet _wallet;
        private readonly BattleEvents _battleEvents;
        private readonly BattleStarter _battleStarter;
        private readonly GameExiter _gameExiter;
        private readonly PlayerProgressEvents _playerProgressEvents;

        public event Action UpgradesRequested;

        public MainMenuPresenter(MainMenuView mainMenuView, Wallet wallet, BattleEvents battleEvents, BattleStarter battleStarter, GameExiter gameExiter, PlayerProgressEvents playerProgressEvents)
        {
            _mainMenuView = mainMenuView;
            _wallet = wallet;
            _battleEvents = battleEvents;
            _battleStarter = battleStarter;
            _gameExiter = gameExiter;
            _playerProgressEvents = playerProgressEvents;
        }

        public void Initialize()
        {
            _mainMenuView.StartGameButton.onClick.AddListener(StartGame);

            _mainMenuView.ExitGameButton.onClick.AddListener(_gameExiter.ExitGame);

            _mainMenuView.UpgradesButton.onClick.AddListener(OpenUpgrades);

            _battleEvents.BattleEnded += OnBattleEnded;

            _playerProgressEvents.ProgressChanged += OnProgressChanged;

            OnProgressChanged();
        }

        public void ShowMainMenu()
        {
            _mainMenuView.ShowMainMenuUI();
        }

        private void StartGame()
        {
            _mainMenuView.HideMainMenuUI();

            _battleStarter.Start();
        }

        private void OpenUpgrades()
        {
            _mainMenuView.HideMainMenuUI();
            UpgradesRequested?.Invoke();
        }

        private void OnProgressChanged()
        {
            _mainMenuView.SetCoins(_wallet.Coins);
        }

        private void OnBattleEnded()
        {
            _mainMenuView.ShowMainMenuUI();
        }

        public void Dispose()
        {
            _mainMenuView.StartGameButton.onClick.RemoveListener(StartGame);

            _mainMenuView.ExitGameButton.onClick.RemoveListener(_gameExiter.ExitGame);

            _mainMenuView.UpgradesButton.onClick.RemoveListener(OpenUpgrades);

            _playerProgressEvents.ProgressChanged += OnProgressChanged;

            _battleEvents.BattleEnded -= OnBattleEnded;
        }
    }
}