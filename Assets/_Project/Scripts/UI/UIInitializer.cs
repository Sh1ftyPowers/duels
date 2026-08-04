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
            var battleView = _factory.CreateBattleCanvas();
            var restartView = _factory.CreateRestartCanvas();

            _container.BindInstance(battleView);
            _container.BindInstance(restartView);

            var presenter = _container.Instantiate<BattlePresenter>();
            var restarter = _container.Instantiate<GameRestarter>();

            presenter.Initialize();
            restarter.Initialize();
        }
    }
}