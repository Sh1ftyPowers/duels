using Duels.Effects;
using Duels.Units;

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
            return damage - DamageReduction;
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