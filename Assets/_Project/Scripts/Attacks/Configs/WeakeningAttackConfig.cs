using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Configs/Attacks/WeakeningAttackConfig")]
    public class WeakeningAttackConfig : ScriptableObject
    {
        [Range(0, 1)]
        public float ChanceToWeaken = 0.3f;
        public int DamageReduction = 5;
    }
}
