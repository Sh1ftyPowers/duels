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
            message.ShowMessageText(target.UnitName + " is stunned!");

            UnityEngine.Debug.Log($"{target.UnitName} оглушен");
        }

        public override void OnTurnStart(Unit target)
        {
            Duration--;
        }
    }
}