using Duels.Effects;
using Duels.Units;

namespace Duels.Attacks
{
    public class MageAttack : BaseAttack
    {
        private float _chanceToWeaken = 0.3f;

        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        {
            attacker.PlayAttackAnimation();

            target.TakeDamage(attacker.Damage);

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
