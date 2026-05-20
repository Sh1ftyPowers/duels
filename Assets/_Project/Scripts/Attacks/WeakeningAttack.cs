using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Attacks
{
    public class WeakeningAttack : StatusEffect
    {
        private const int WeakeningEffectDuration = 2;
        private int _damageReduction;

        public int DamageReduction => _damageReduction;

        public WeakeningAttack(int damageReductionValue)
        {
            _damageReduction = damageReductionValue;
            Duration = WeakeningEffectDuration;
        }

        public override void Apply(Unit target, MessageSystem message)
        {
            message.ShowMessageText(target.UnitName + " is weakened!");
            UnityEngine.Debug.Log($"{target.UnitName} ослаблен");
        }

        public override void OnTurnStart(Unit target)
        {
            Duration--;
        }
    }
}