using Duels.Effects;
using Duels.Units;
using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Attacks/Warrior Attack")]
    public class WarriorAttack : BaseAttack
    {
        private float _chanceToStun = 0.3f;
    
        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        {
            attacker.PlayAttackAnimation();

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