using UnityEngine;
using Zenject;
using Duels.Presentation;
using Duels.UI;

namespace Duels.Core
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuViewPrefab;
        [SerializeField] private UpgradeView _upgradeViewViewPrefab;

        public override void InstallBindings()
        {
            BindUI();
            BindPresenters();
        }

        private void BindUI()
        {
            Container.Bind<MainMenuView>().FromComponentInNewPrefab(_mainMenuViewPrefab).AsSingle();

            Container.Bind<UpgradeView>().FromComponentInNewPrefab(_upgradeViewViewPrefab).AsSingle();
        }

        private void BindPresenters()
        {
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<UpgradePresenter>().AsSingle();

            Container.Bind<BattleStarter>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameExiter>().AsSingle();

            Container.BindInterfacesAndSelfTo<MenuController>().AsSingle();
        }
    }
}