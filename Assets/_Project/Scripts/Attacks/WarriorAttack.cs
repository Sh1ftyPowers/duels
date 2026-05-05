using Duels.Effects;
using Duels.Units;

namespace Duels.Attacks
{
    public class WarriorAttack : BaseAttack
    {
        private float _chanceToStun = 0.3f;
    
        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        {
            attacker.PlayAttackAnimation();
            target.TakeDamage(attacker.Damage);

            StatusEffect effect = null;

            if (UnityEngine.Random.value < _chanceToStun)
            {
                effect = new StunningAttack();
            }

            return new AttackResult
            {
                Damage = attacker.Damage,
                Effect = effect
            };
        }
    }
}