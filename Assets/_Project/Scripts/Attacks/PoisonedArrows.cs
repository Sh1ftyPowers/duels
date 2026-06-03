using Duels.Effects;
using Duels.Units;

namespace Duels.Attacks
{
    public class PoisonedArrows : StatusEffect
    {
        public override string EffectName => "poisoned";
        private int _poisonDamage;
        private const int PoisonEffectDuration = 2;

        public PoisonedArrows(int damage)
        {
            _poisonDamage = damage;
            Duration = PoisonEffectDuration;
        }

        public override void Apply(Unit target)
        {
            UnityEngine.Debug.Log($"{target.UnitName} отравлен");
        }

        public override void OnTurnStart(Unit target)
        {
            target.TakePoisonDamage(_poisonDamage);
            Duration--;
        }
    }
}