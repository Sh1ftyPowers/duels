using Duels.Core;
using Duels.Presentation;
using Zenject;

namespace Duels.UI
{
    public class UIInitializer : IInitializable
    {
        private readonly UIFactory _factory;
        private readonly DiContainer _container;

        public UIInitializer(UIFactory factory, DiContainer container)
        {
            _factory = factory;
            _container = container;
        }

        public void Initialize()
        {
            RegisterViews();
            CreatePresenters();
        }

        private void RegisterViews()
        {
            var battleView = _factory.CreateBattleCanvas();
            var restartView = _factory.CreateRestartCanvas();

            _container.BindInstance(battleView);
            _container.BindInstance(restartView);
        }

        private void CreatePresenters()
        {
            _container.Instantiate<BattlePresenter>().Initialize();
            _container.Instantiate<GameRestarter>().Initialize();
        }
    }
}