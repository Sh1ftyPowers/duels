using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Attacks
{
    public class WeakeningAttack : StatusEffect
    {
        private int _reduction;

        public WeakeningAttack(int reduction)
        {
            _reduction = reduction;
            Duration = 2;
        }

        public override void Apply(Unit target, MessageSystem message)
        {
            target.IsWeakened = true;
            target.DamageReduction = _reduction;

            message.ShowMessageText(target.UnitName + " is weakened!");
        }

        public override void OnTurnStart(Unit target)
        {
            Duration--;
        }

        public override void Remove(Unit target)
        {
            target.IsWeakened = false; 
            target.DamageReduction = 0;
        }
    }
}
