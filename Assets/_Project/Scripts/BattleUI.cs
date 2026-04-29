using TMPro;
using UnityEngine;

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
