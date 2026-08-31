using UnityEngine;

namespace Duels.Economy
{
    [CreateAssetMenu(menuName = "Configs/WalletConfig")]
    public class WalletConfig : ScriptableObject
    {
        [field: SerializeField] public int StartingCoins { get; private set; } = 100;
        [field: SerializeField] public int VictoryReward { get; private set; } = 100;
    }
}