using Duels.Effects;
using Duels.Units;

namespace Duels.Attacks
{
    public class StunningAttack : StatusEffect
    {
        public override string EffectName => "stunned";
        private const int StunEffectDuration = 2;

        public StunningAttack()
        {
            Duration = StunEffectDuration;
        }

        public override void Apply(Unit target)
        {
            UnityEngine.Debug.Log($"{target.UnitName} оглушен");
        }

        public override void OnTurnStart(Unit target)
        {
            Duration--;
        }
    }
}