using Duels.Effects;
using Duels.Units;

namespace Duels.Attacks
{
    public class WeakeningAttack : StatusEffect
    {
        private const int WeakeningEffectDuration = 2;
        private int _damageReduction;
        private string _name = "weakened";

        public int DamageReduction => _damageReduction;

        public WeakeningAttack(int damageReductionValue)
        {
            _damageReduction = damageReductionValue;
            Duration = WeakeningEffectDuration;
            EffectName = _name;
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