using UnityEngine;
using TMPro;

namespace Duels.UI
{
    public class BattleView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _turnInfoText;
        [SerializeField] private TextMeshProUGUI _statusInfoText;

        [SerializeField] private GameObject _gameOverCanvas;

        public void SetTurnText(string turnText)
        {
            _turnInfoText.text = turnText;
        }

        public void SetStatusText(string statusText)
        {
            _statusInfoText.text = statusText;
        }

        public void ShowRestart()
        {
            _gameOverCanvas.SetActive(true);
        }
    }
}