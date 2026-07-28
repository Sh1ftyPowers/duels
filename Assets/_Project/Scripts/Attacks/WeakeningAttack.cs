using Duels.Effects;
using Duels.Units;
using UnityEngine;

namespace Duels.Attacks
{
    public class WeakeningAttack : StatusEffect
    {
        public int DamageReduction { get; }

        public override string EffectName => "weakened";
        private const int WeakeningEffectDuration = 2;

        public WeakeningAttack(int damageReductionValue)
        {
            DamageReduction = damageReductionValue;
            Duration = WeakeningEffectDuration;
        }

        public override int ModifyDamage(int damage)
        {
            return Mathf.Max(0, damage - DamageReduction);
        }

        public override void Apply(Unit target)
        {
            UnityEngine.Debug.Log($"{target.UnitName} ослаблен");
        }

        public override void OnTurnStart(Unit target)
        {
            Duration--;
        }
    }
}