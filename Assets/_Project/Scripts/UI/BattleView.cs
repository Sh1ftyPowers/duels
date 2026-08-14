using UnityEngine;
using TMPro;

namespace Duels.UI
{
    public class BattleView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _turnInfoText;
        [SerializeField] private TextMeshProUGUI _statusInfoText;

        public void SetTurnText(string turnText)
        {
            _turnInfoText.text = turnText;
        }

        public void SetStatusText(string statusText)
        {
            _statusInfoText.text = statusText;
        }

        public void HideBattleUI()
        {
            gameObject.SetActive(false);
        }

        public void ShowBattleUI()
        {
            gameObject.SetActive(true);
        }

        public void Reset()
        {
            SetTurnText(string.Empty);
            SetStatusText("No active negative effects");
        }
    }
}