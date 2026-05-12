using TMPro;
using UnityEngine;

namespace Duels.UI
{
    public class BattleUI : MonoBehaviour
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
    }
}
