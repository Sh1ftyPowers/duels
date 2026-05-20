using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Attacks
{
    public class PoisonedArrows : StatusEffect
    {
        private int _poisonDamage;
        private const int PoisonEffectDuration = 2;

        public PoisonedArrows(int damage)
        {
            _poisonDamage = damage;
            Duration = PoisonEffectDuration;
        }

        public override void Apply(Unit target, MessageSystem message)
        {
            message.ShowMessageText(target.UnitName + " is poisoned!");
            UnityEngine.Debug.Log($"{target.UnitName} отравлен");
        }

        public override void OnTurnStart(Unit target)
        {
            target.TakePoisonDamage(_poisonDamage);
            Duration--;
        }
    }
}