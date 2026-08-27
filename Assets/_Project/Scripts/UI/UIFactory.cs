using Zenject;

namespace Duels.UI
{
    public class UIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly BattleView _battleViewPrefab;

        public UIFactory(IInstantiator instantiator, BattleView battleViewPrefab)
        {
            _instantiator = instantiator;
            _battleViewPrefab = battleViewPrefab;
        }

        public BattleView CreateBattleCanvas()
        {
            var battleView = _instantiator.InstantiatePrefabForComponent<BattleView>(_battleViewPrefab);

            battleView.HideBattleUI();

            return battleView;
        }
    }
}