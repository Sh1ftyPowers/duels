using Duels.Effects;
using Duels.Units;
using UnityEngine;

namespace Duels.Attacks
{
    [CreateAssetMenu(menuName = "Attacks/Mage Attack")]
    public class MageAttack : BaseAttack
    {
        [SerializeField] private WeakeningAttackConfig _config;

        public override AttackResult AttackEnemy(Unit attacker, Unit target)
        {
            attacker.UnitAnimationManager.PlayAttackAnimation();

            StatusEffect effect = null;

            if (UnityEngine.Random.value < _config.ChanceToWeaken)
            {
                effect = new WeakeningAttack(_config.DamageReduction);
            }

            return new AttackResult
            {
                Damage = attacker.Damage,
                Effect = effect
            };
        }
    }
}