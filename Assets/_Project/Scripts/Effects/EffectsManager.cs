using Duels.Units;
using System;
using System.Linq;

namespace Duels.Effects
{
    public class EffectsManager
    {
        public event Action<Unit, StatusEffect> EffectApplied;
        public event Action<Unit, StatusEffect> EffectExpired;

        public void ApplyEffect(Unit unit, StatusEffect effect)
        {
            unit.AddEffect(effect);

            effect.Apply(unit);

            EffectApplied?.Invoke(unit, effect);
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

                    EffectExpired?.Invoke(unit, effect);
                } 
            }
        }
    }
}