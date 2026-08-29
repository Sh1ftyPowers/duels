using Duels.Core;
using Duels.Presentation;
using Duels.UI;

namespace Duels.Units
{
    public class UnitFactory
    {
        private readonly UnitSpawner _unitSpawner;
        private readonly HealthbarPresenter _healthbarPresenter;
        private readonly UpgradeService _upgradeService;

        public UnitFactory(UnitSpawner unitSpawner, HealthbarPresenter healthbarPresenter, UpgradeService upgradeService)
        {
            _unitSpawner = unitSpawner;
            _healthbarPresenter = healthbarPresenter;
            _upgradeService = upgradeService;
        }

        public Unit CreateEnemyTeamHero()
        {
            return CreateHero(_unitSpawner.SpawnEnemyTeamHero());
        }

        public Unit CreatePlayerTeamHero()
        {
            Unit playerHero = _unitSpawner.SpawnPlayerTeamHero();

            playerHero.ApplyUpgrades(_upgradeService.HealthMultiplier, _upgradeService.DamageMultiplier, _upgradeService.AttackSpeedMultiplier);

            return CreateHero(playerHero);
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