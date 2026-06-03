using Duels.Effects;
using Duels.Units;

namespace Duels.Attacks
{
    public class WeakeningAttack : StatusEffect
    {
        public override string EffectName => "weakened";
        private const int WeakeningEffectDuration = 2;
        private int _damageReduction;

        public int DamageReduction => _damageReduction;

        public WeakeningAttack(int damageReductionValue)
        {
            _damageReduction = damageReductionValue;
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