using Zenject;

namespace Duels.UI
{
    public class UIFactory
    {
        private readonly DiContainer _container;

        private readonly BattleView _battlePrefab;
        private readonly RestartView _restartPrefab;


        public UIFactory(DiContainer container, BattleView battlePrefab, RestartView restartPrefab)
        {
            _container = container;

            _battlePrefab = battlePrefab;
            _restartPrefab = restartPrefab;
        }

        public BattleView CreateBattleCanvas()
        {
            return _container.InstantiatePrefabForComponent<BattleView>(_battlePrefab);
        }

        public RestartView CreateRestartCanvas()
        {
            return _container.InstantiatePrefabForComponent<RestartView>(_restartPrefab);
        }
    }
}