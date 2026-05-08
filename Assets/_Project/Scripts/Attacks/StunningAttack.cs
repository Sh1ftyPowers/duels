using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Attacks
{
    public class StunningAttack : StatusEffect
    {
        public StunningAttack()
        {
            Duration = 2;
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
