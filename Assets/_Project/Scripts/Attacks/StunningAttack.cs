using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Attacks
{
    public class StunningAttack : StatusEffect
    {
        private int _stunDuration = 2;

        public StunningAttack()
        {
            Duration = _stunDuration;
        }

        public override void Apply(Unit target, MessageSystem message)
        {
            target.ApplyStun();

            target.PlayStunAnimation();

            message.ShowMessageText(target.UnitName + " is stunned!");
        }

        public override void OnTurnStart(Unit target)
        {
            _stunDuration--;
        }

        public override void Remove(Unit target)
        {
            target.RemoveStun();
        }
    }
}
