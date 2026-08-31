using Duels.Core;
using Duels.Economy;

namespace Duels.Save
{
    public class PlayerDataService
    {
        private readonly Wallet _wallet;
        private readonly UpgradeService _upgradeService;
        private readonly ISaveService _saveService;

        public PlayerDataService(Wallet wallet, UpgradeService upgradeService, ISaveService saveService)
        {
            _wallet = wallet;
            _upgradeService = upgradeService;
            _saveService = saveService;
        }

        public void Save()
        {
            var saveData = new SaveData
            {
                Coins = _wallet.Coins,

                HealthUpgradeLevel = _upgradeService.HealthLevel,
                DamageUpgradeLevel = _upgradeService.DamageLevel,
                AttackSpeedUpgradeLevel = _upgradeService.AttackSpeedLevel
            };

            _saveService.Save(saveData);
        }

        public void Load()
        {
            SaveData saveData = _saveService.Load();

            if (saveData == null)
                return;

            _wallet.SetCoins(saveData.Coins);

            _upgradeService.SetLevels(saveData.HealthUpgradeLevel, saveData.DamageUpgradeLevel, saveData.AttackSpeedUpgradeLevel);
        }
    }
}