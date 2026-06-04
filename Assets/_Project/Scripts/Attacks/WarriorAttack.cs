using Duels.Effects;
using Duels.Units;
using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Attacks/Warrior Attack")]
    public class WarriorAttack : BaseAttack
    {
        [SerializeField] private StunningAttackConfig _config;
    
        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        {
            attacker.UnitAnimationManager.PlayAttackAnimation();

            StatusEffect effect = null;

            if (UnityEngine.Random.value < _config.ChanceToStun)
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