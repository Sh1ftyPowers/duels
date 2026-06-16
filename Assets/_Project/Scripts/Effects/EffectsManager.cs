using System.Linq;
using Duels.UI;
using Duels.Units;

namespace Duels.Effects
{
    public class EffectsManager
    {
        private readonly MessageSystem _message;

        public EffectsManager(MessageSystem message)
        {
            _message = message;
        }

        public void ApplyEffect(Unit unit, StatusEffect effect)
        {
            unit.AddEffect(effect);
            effect.Apply(unit);

            _message.ShowMessageText($"{unit.UnitName} is {effect.EffectName}");
        }

        public void ProcessEffects(Unit unit)
        {
            var effects = unit.ActiveEffects;

            if (effects.Count == 0)
                return;

            foreach(var effect in effects.ToList()) 
            { 
                effect.OnTurnStart(unit); 
                    
                if (effect.Duration <= 0) 
                { 
                    effect.Remove(unit); 
                    unit.RemoveEffect(effect);

                    _message.ShowMessageText($"{unit.UnitName} lost an effect"); 
                } 
            }
        }
    }
}