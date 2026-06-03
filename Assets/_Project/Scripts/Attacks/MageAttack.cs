using Duels.Effects;
using Duels.Units;
using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Attacks/Mage Attack")]
    public class MageAttack : BaseAttack
    {
        [Range(0, 1)]
        [field:SerializeField] private float _chanceToWeaken = 0.3f;
        [field: SerializeField] private int _damageReduction = 5;

        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        {
            attacker.UnitAnimationManager.PlayAttackAnimation();

            StatusEffect effect = null;

            if (UnityEngine.Random.value < _chanceToWeaken)
            {
                effect = new WeakeningAttack(_damageReduction);
            }

            return new AttackResult
            {
                Damage = attacker.Damage,
                Effect = effect
            };
        }
    }
}