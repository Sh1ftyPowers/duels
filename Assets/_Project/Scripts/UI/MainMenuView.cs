using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Duels.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitGameButton;

        public Button StartGameButton => _startGameButton;
        public Button ExitGameButton => _exitGameButton;

        public void SetCoins(int coins)
        {
            _coinsText.text = $"Coins: {coins}";
        }

        public void HideMainMenuUI()
        {
            gameObject.SetActive(false);
        }

        public void ShowMainMenuUI()
        {
            gameObject.SetActive(true);
        }
    }
}