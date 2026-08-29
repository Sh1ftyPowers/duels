using System;
using Zenject;
using Duels.Presentation;

namespace Duels.UI
{
    public class MenuController : IInitializable, IDisposable
    {
        private readonly MainMenuPresenter _mainMenuPresenter;
        private readonly UpgradePresenter _upgradePresenter;

        public MenuController(MainMenuPresenter mainMenuPresenter, UpgradePresenter upgradePresenter)
        {
            _mainMenuPresenter = mainMenuPresenter;
            _upgradePresenter = upgradePresenter;
        }

        public void Initialize()
        {
            _mainMenuPresenter.UpgradesRequested += OnUpgradesRequested;
            _upgradePresenter.MainMenuRequested += OnMainMenuRequested;
        }

        private void OnUpgradesRequested()
        {
            _upgradePresenter.ShowUpgradeMenu();
        }

        private void OnMainMenuRequested()
        {
            _mainMenuPresenter.ShowMainMenu();
        }

        public void Dispose()
        {
            _mainMenuPresenter.UpgradesRequested -= OnUpgradesRequested;
            _upgradePresenter.MainMenuRequested -= OnMainMenuRequested;
        }
    }
}