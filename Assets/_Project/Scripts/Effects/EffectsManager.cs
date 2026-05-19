using System.Linq;
using Duels.UI;
using Duels.Units;
using UnityEngine;

namespace Duels.Effects
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private MessageSystem _message;

        public void ApplyEffect(Unit unit, StatusEffect effect)
        {
            unit.AddEffect(effect);
            effect.Apply(unit, _message);
        }

        public void ProcessEffects(Unit unit)
        {
            var effects = unit.Effects.GetEffects();

            if (effects.Count == 0)
                return;

            foreach(var effect in effects.ToList()) 
            { 
                effect.OnTurnStart(unit); 
                    
                if (effect.Duration <= 0) 
                { 
                    effect.Remove(unit); 
                    effects.Remove(effect);

                    _message.ShowMessageText($"{unit.UnitName} lost an effect"); 
                } 
            }
        }
    }
}
