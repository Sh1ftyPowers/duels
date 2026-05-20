using System.Collections.Generic;

namespace Duels.Effects
{
    public class EffectsHolder
    {
        private List<StatusEffect> _effects = new List<StatusEffect>();

        public void AddEffect(StatusEffect effect)
        {
            _effects.Add(effect);
        }

        public void RemoveEffect(StatusEffect effect)
        {
            _effects.Remove(effect);
        }

        public List<StatusEffect> GetEffects()
        {
            return _effects;
        }

        public bool HasEffect<EffectType>() where EffectType : StatusEffect
        {
            foreach (StatusEffect effect in _effects)
            {
                if (effect is EffectType)
                {
                    return true;
                }
            }

            return false;
        }

        public EffectType GetEffect<EffectType>() where EffectType : StatusEffect
        {
            foreach (StatusEffect effect in _effects)
            {
                if (effect is EffectType typedEffect)
                {
                    return typedEffect;
                }
            }

            return null;
        }
    }
}