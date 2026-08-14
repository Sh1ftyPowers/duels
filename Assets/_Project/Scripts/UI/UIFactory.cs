using Zenject;

namespace Duels.UI
{
    public class UIFactory
    {
        private readonly DiContainer _container;
        private readonly BattleView _battleViewPrefab;

        public UIFactory(DiContainer container, BattleView battleViewPrefab)
        {
            _container = container;
            _battleViewPrefab = battleViewPrefab;
        }

        public BattleView CreateBattleCanvas()
        {
            var battleView = _container.InstantiatePrefabForComponent<BattleView>(_battleViewPrefab);

            battleView.HideBattleUI();

            return battleView;
        }
    }
}