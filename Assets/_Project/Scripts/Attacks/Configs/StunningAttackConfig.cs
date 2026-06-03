using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Configs/Attacks/StunningAttackConfig")]
    public class StunningAttackConfig : ScriptableObject
    {
        [Range(0, 1)]
        public float ChanceToStun = 0.3f;
    }
}
