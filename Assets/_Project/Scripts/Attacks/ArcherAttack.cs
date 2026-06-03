using Duels.Units;
using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Attacks/Archer Attack")]
    public class ArcherAttack : BaseAttack
    {
        [SerializeField] private PoisonedArrowsConfig _config;

        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        {
            attacker.UnitAnimationManager.PlayAttackAnimation();

            return new AttackResult
            {
                Damage = attacker.Damage,
                Effect = new PoisonedArrows(_config.PoisonDamage)
            };
        }
    }
}