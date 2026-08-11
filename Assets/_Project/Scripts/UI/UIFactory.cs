using Zenject;

namespace Duels.UI
{
    public class UIFactory
    {
        private readonly DiContainer _container;
        private readonly BattleView _battleViewPrefab;
        private readonly RestartView _restartViewPrefab;

        public UIFactory(DiContainer container, BattleView battleViewPrefab, RestartView restartViewPrefab)
        {
            _container = container;
            _battleViewPrefab = battleViewPrefab;
            _restartViewPrefab = restartViewPrefab;
        }

        public BattleView CreateBattleCanvas()
        {
            return _container.InstantiatePrefabForComponent<BattleView>(_battleViewPrefab);
        }

        public RestartView CreateRestartCanvas()
        {
            return _container.InstantiatePrefabForComponent<RestartView>(_restartViewPrefab);
        }
    }
}