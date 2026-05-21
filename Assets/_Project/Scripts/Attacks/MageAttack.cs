using Duels.Effects;
using Duels.Units;
using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Attacks/Mage Attack")]
    public class MageAttack : BaseAttack
    {
        private float _chanceToWeaken = 0.3f;

        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        {
            attacker.PlayAttackAnimation();

            StatusEffect effect = null;

            if (UnityEngine.Random.value < _chanceToWeaken)
            {
                effect = new WeakeningAttack(5);
            }

            return new AttackResult
            {
                Damage = attacker.Damage,
                Effect = effect
            };
        }
    }
}