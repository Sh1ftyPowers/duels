using System;
using Zenject;
using Duels.Core;
using Duels.Economy;

namespace Duels.Save
{
    public class GameSaveController : IInitializable, IDisposable
    {
        private readonly Wallet _wallet;
        private readonly UpgradeService _upgradeService;
        private readonly PlayerDataService _playerDataService;
        private readonly GameExiter _gameExiter;

        public GameSaveController(Wallet wallet, UpgradeService upgradeService, PlayerDataService playerDataService, GameExiter gameExiter)
        {
            _wallet = wallet;
            _upgradeService = upgradeService;
            _playerDataService = playerDataService;
            _gameExiter = gameExiter;
        }

        public void Initialize()
        {
            _wallet.WalletChanged += OnWalletChanged;
            _upgradeService.UpgradePurchased += OnUpgradePurchased;
            _gameExiter.ExitRequested += OnExitRequested;
        }

        private void OnWalletChanged()
        {
            _playerDataService.Save();
        }

        private void OnUpgradePurchased()
        {
            _playerDataService.Save();
        }

        private void OnExitRequested()
        {
            _playerDataService.Save();
        }

        public void Dispose()
        {
            _wallet.WalletChanged -= OnWalletChanged;
            _upgradeService.UpgradePurchased -= OnUpgradePurchased;
            _gameExiter.ExitRequested -= OnExitRequested;
        }
    }
}