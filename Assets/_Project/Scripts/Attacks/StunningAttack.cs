using Duels.Effects;
using Duels.Units;

namespace Duels.Attacks
{
    public class StunningAttack : StatusEffect
    {
        private const int StunEffectDuration = 2;
        private string _name = "stunned";

        public StunningAttack()
        {
            Duration = StunEffectDuration;
            EffectName = _name;
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