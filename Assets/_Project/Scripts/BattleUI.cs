using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI turnInfoText;
    [SerializeField] public TextMeshProUGUI statusInfoText;

    public void SetTurnText(string turnText)
    {
        turnInfoText.text = turnText;
    }

    public void SetStatusText(string statusText)
    {
        statusInfoText.text = statusText;
    }
}
