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

        private void CreateHero(Unit hero)
        {
            hero.Initialize();

            _healthbarPresenter.RegisterUnit(hero, hero.GetComponentInChildren<Healthbar>());
        }

        public Unit CreateTeamOneHero()
        {
            Unit teamOneHero = _unitSpawner.SpawnTeamOne();

            CreateHero(teamOneHero);

            return teamOneHero;
        }

        public Unit CreateTeamTwoHero()
        {
            Unit teamTwoHero = _unitSpawner.SpawnTeamTwo();

            CreateHero(teamTwoHero);

            return teamTwoHero;
        }
    }
}