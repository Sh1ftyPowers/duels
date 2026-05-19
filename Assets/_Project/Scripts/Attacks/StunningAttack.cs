using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Attacks
{
    public class StunningAttack : StatusEffect
    {
        private const int StunEffectDuration = 2;

        public StunningAttack()
        {
            Duration = StunEffectDuration;
        }

        public override void Apply(Unit target, MessageSystem message)
        {
            target.ApplyStun();

            target.PlayStunAnimation();

            message.ShowMessageText(target.UnitName + " is stunned!");
        }

        public override void OnTurnStart(Unit target)
        {
            Duration--;
        }

        public override void Remove(Unit target)
        {
            target.RemoveStun();
        }
    }
}
