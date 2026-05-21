using Duels.Units;
using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Attacks/Archer Attack")]
    public class ArcherAttack : BaseAttack
    {
        private int _poisonDamage = 5;

        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        { 
            attacker.PlayAttackAnimation();

            return new AttackResult
            {
                Damage = attacker.Damage,
                Effect = new PoisonedArrows(_poisonDamage)
            };
        }
    }
}