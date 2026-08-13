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

        public Unit CreateTeamOneHero()
        {
            return CreateHero(_unitSpawner.SpawnEnemyTeamHero());
        }

        public Unit CreateTeamTwoHero()
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
    }
}