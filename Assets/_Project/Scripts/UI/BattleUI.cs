using TMPro;
using UnityEngine;

namespace Duels.UI
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI TurnInfoText;
        [SerializeField] public TextMeshProUGUI StatusInfoText;

        public void SetTurnText(string turnText)
        {
            TurnInfoText.text = turnText;
        }

        public void SetStatusText(string statusText)
        {
            StatusInfoText.text = statusText;
        }
    }
}
