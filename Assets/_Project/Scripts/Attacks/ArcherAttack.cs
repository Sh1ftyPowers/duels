using Duels.Units;

namespace Duels.Attacks
{
    public class ArcherAttack : BaseAttack
    {
        private int _poisonDamagePerTick = 2;

        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        { 
            attacker.PlayAttackAnimation();

            target.TakeDamage(attacker.Damage);

            return new AttackResult
            {
                Damage = attacker.Damage,
                Effect = new PoisonedArrows(_poisonDamagePerTick)
            };
        }
    }
}