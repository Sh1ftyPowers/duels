using System;
using Zenject;
using Duels.UI;
using Duels.Core;

namespace Duels.Presentation
{
    public class UpgradePresenter : IInitializable, IDisposable
    {
        private readonly UpgradeView _upgradeView;
        private readonly UpgradeService _upgradeService;
        private readonly Wallet _wallet;

        public event Action MainMenuRequested;

        public UpgradePresenter(UpgradeView upgradeView, UpgradeService upgradeService, Wallet wallet)
        {
            _upgradeView = upgradeView;
            _upgradeService = upgradeService;
            _wallet = wallet;
        }

        public void Initialize()
        {
            _upgradeView.HealthUpgradeButton.onClick.AddListener(UpgradeHealth);
            _upgradeView.DamageUpgradeButton.onClick.AddListener(UpgradeDamage);
            _upgradeView.AttackSpeedUpgradeButton.onClick.AddListener(UpgradeAttackSpeed);
            _upgradeView.BackToMainMenuButton.onClick.AddListener(BackToMainMenu);

            _wallet.CoinsChanged += OnCoinsChanged;

            UpdateUpgradeView();

            OnCoinsChanged(_wallet.Coins);

            _upgradeView.HideUpgradeMenuUI();
        }

        public void ShowUpgradeMenu()
        {
            UpdateUpgradeView();
            _upgradeView.ShowUpgradeMenuUI();
        }

        private void OnCoinsChanged(int coins)
        {
            _upgradeView.SetCoins(coins);
        }

        private void UpgradeHealth()
        {
            if (!_upgradeService.TryUpgradeHealth())
                return;

            UpdateUpgradeView();
        }

        private void UpgradeDamage()
        {
            if (!_upgradeService.TryUpgradeDamage())
                return;

            UpdateUpgradeView();
        }

        private void UpgradeAttackSpeed()
        {
            if (!_upgradeService.TryUpgradeAttackSpeed())
                return;

            UpdateUpgradeView();
        }

        private void BackToMainMenu()
        {
            _upgradeView.HideUpgradeMenuUI();
            MainMenuRequested?.Invoke();
        }

        private void UpdateUpgradeView()
        {
            _upgradeView.SetHealthUpgradeText(_upgradeService.HealthLevel + 1, _upgradeService.GetHealthUpgradeCost());
            _upgradeView.SetDamageUpgradeText(_upgradeService.DamageLevel + 1, _upgradeService.GetDamageUpgradeCost());
            _upgradeView.SetAttackSpeedUpgradeText(_upgradeService.AttackSpeedLevel + 1, _upgradeService.GetAttackSpeedUpgradeCost());
        }

        public void Dispose()
        {
            _upgradeView.HealthUpgradeButton.onClick.RemoveListener(UpgradeHealth);
            _upgradeView.DamageUpgradeButton.onClick.RemoveListener(UpgradeDamage);
            _upgradeView.AttackSpeedUpgradeButton.onClick.RemoveListener(UpgradeAttackSpeed);
            _upgradeView.BackToMainMenuButton.onClick.RemoveListener(BackToMainMenu);
            _wallet.CoinsChanged -= OnCoinsChanged;
        }
    }
}