using Duels.Presentation;
using Duels.UI;

namespace Duels.Units
{
    public class UnitFactory
    {
        private readonly UnitSpawner _unitSpawner;
        private readonly HealthbarPresenter _healthbarPresenter;

        public UnitFactory(UnitSpawner unitSpawner, HealthbarPresenter healthbarPresenter)
        {
            _unitSpawner = unitSpawner;
            _healthbarPresenter = healthbarPresenter;
        }

        public Unit CreateEnemyTeamHero()
        {
            return CreateHero(_unitSpawner.SpawnEnemyTeamHero());
        }

        public Unit CreateHeroTeamHero()
        {
            return CreateHero(_unitSpawner.SpawnPlayerTeamHero());
        }

        private Unit CreateHero(Unit hero)
        {
            hero.Initialize();

            Healthbar healthbar = hero.GetComponentInChildren<Healthbar>();

            _healthbarPresenter.RegisterUnit(hero, healthbar);

            return hero;
        }

        public void ClearUnits()
        {
            foreach (Unit unit in _unitSpawner.SpawnedUnits)
            {
                _healthbarPresenter.UnregisterUnit(unit);
            }

            _unitSpawner.ClearUnits();
        }
    }
}