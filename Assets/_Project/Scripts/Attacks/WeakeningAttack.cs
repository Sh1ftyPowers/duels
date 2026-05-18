using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Attacks
{
    public class WeakeningAttack : StatusEffect
    {
        private int _damageReduction;
        private const int WeakeningEffectLifetime = 2;

        public WeakeningAttack(int damageReductionValue)
        {
            _damageReduction = damageReductionValue;
            Duration = WeakeningEffectLifetime;
        }

        public override void Apply(Unit target, MessageSystem message)
        {
            target.ApplyWeakness(_damageReduction);

            message.ShowMessageText(target.UnitName + " is weakened!");
        }

        public override void OnTurnStart(Unit target)
        {
            Duration--;
        }

        public override void Remove(Unit target)
        {
            target.RemoveWeakness();
        }
    }
}