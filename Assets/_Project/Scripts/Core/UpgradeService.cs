namespace Duels.Core
{
    public class UpgradeService
    {
        private readonly Wallet _wallet;

        private const int BaseUpgradeCost = 100;
        private const int UpgradeCostIncrease = 20;

        private const float HealthMultiplierIncreasePerLevel = 0.1f;
        private const float DamageMultiplierIncreasePerLevel = 0.1f;
        private const float AttackSpeedMultiplierIncreasePerLevel = 0.1f;

        private int _healthLevel;
        private int _damageLevel;
        private int _attackSpeedLevel;

        public int HealthLevel => _healthLevel;
        public int DamageLevel => _damageLevel;
        public int AttackSpeedLevel => _attackSpeedLevel;

        public float HealthMultiplier => 1f + _healthLevel * HealthMultiplierIncreasePerLevel;
        public float DamageMultiplier => 1f + _damageLevel * DamageMultiplierIncreasePerLevel;
        public float AttackSpeedMultiplier => 1f + _attackSpeedLevel * AttackSpeedMultiplierIncreasePerLevel;

        public UpgradeService(Wallet wallet)
        {
            _wallet = wallet;
        }

        public bool TryUpgradeHealth()
        {
            int healthUpgradeCost = GetHealthUpgradeCost();

            if(!_wallet.TrySpendCoins(healthUpgradeCost))
                return false;

            _healthLevel++;

            return true;
        }

        public bool TryUpgradeDamage()
        {
            int damageUpgradeCost = GetDamageUpgradeCost();

            if (!_wallet.TrySpendCoins(damageUpgradeCost))
                return false;

            _damageLevel++;

            return true;
        }

        public bool TryUpgradeAttackSpeed()
        {
            int attackSpeedUpgradeCost = GetAttackSpeedUpgradeCost();

            if (!_wallet.TrySpendCoins(attackSpeedUpgradeCost))
                return false;

            _attackSpeedLevel++;

            return true;
        }

        public int GetHealthUpgradeCost()
        {
            return CalculateUpgradeCost(_healthLevel);
        }

        public int GetDamageUpgradeCost()
        {
            return CalculateUpgradeCost(_damageLevel);
        }

        public int GetAttackSpeedUpgradeCost()
        {
            return CalculateUpgradeCost(_attackSpeedLevel);
        }

        private int CalculateUpgradeCost(int currentLevel)
        {
            return BaseUpgradeCost + currentLevel * UpgradeCostIncrease;
        }
    }
}