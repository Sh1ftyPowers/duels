using UnityEngine;
using Zenject;
using Duels.Presentation;
using Duels.UI;

namespace Duels.Core
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuViewPrefab;

        public override void InstallBindings()
        {
            BindUI();
            BindPresenters();
        }

        private void BindUI()
        {
            Container.Bind<MainMenuView>().FromComponentInNewPrefab(_mainMenuViewPrefab).AsSingle();
        }

        private void BindPresenters()
        {
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStarter>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameExiter>().AsSingle();
        }
    }
}