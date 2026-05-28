using Duels.Effects;
using Duels.Units;

namespace Duels.Attacks
{
    public class PoisonedArrows : StatusEffect
    {
        private int _poisonDamage;
        private const int PoisonEffectDuration = 2;
        private string _name = "poisoned";

        public PoisonedArrows(int damage)
        {
            _poisonDamage = damage;
            Duration = PoisonEffectDuration;
            EffectName = _name;
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