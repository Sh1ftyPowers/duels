using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Duels.UI
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthUpgradeText;
        [SerializeField] private TMP_Text _damageUpgradeText;
        [SerializeField] private TMP_Text _attackSpeedUpgradeText;
        [SerializeField] private TMP_Text _coinsText;

        [SerializeField] private Button _healthUpgradeButton;
        [SerializeField] private Button _damageUpgradeButton;
        [SerializeField] private Button _attackSpeedUpgradeButton;
        [SerializeField] private Button _backToMainMenu;

        public Button HealthUpgradeButton => _healthUpgradeButton;
        public Button DamageUpgradeButton => _damageUpgradeButton;
        public Button AttackSpeedUpgradeButton => _attackSpeedUpgradeButton;
        public Button BackToMainMenuButton => _backToMainMenu;

        public void SetHealthUpgradeText(int currentLevel, int upgradeCost)
        {
            _healthUpgradeText.text = $"Health level: {currentLevel}\nUpgrade cost: {upgradeCost}";
        }

        public void SetDamageUpgradeText(int currentLevel, int upgradeCost)
        {
            _damageUpgradeText.text = $"Damage level: {currentLevel}\nUpgrade cost: {upgradeCost}";
        }

        public void SetAttackSpeedUpgradeText(int currentLevel, int upgradeCost)
        {
            _attackSpeedUpgradeText.text = $"Attack Speed level: {currentLevel}\nUpgrade cost: {upgradeCost}";
        }

        public void SetCoins(int coins)
        {
            _coinsText.text = $"Coins: {coins}";
        }

        public void ShowUpgradeMenuUI()
        {
            gameObject.SetActive(true);
        }

        public void HideUpgradeMenuUI()
        {
            gameObject.SetActive(false);
        }
    }
}