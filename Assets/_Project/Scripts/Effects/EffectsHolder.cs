using System.Collections.Generic;

namespace Duels.Effects
{
    public class EffectsHolder
    {
        private readonly List<StatusEffect> _effects = new();

        public IReadOnlyList<StatusEffect> ActiveEffects => _effects;

        public void AddEffect(StatusEffect effect)
        {
            _effects.Add(effect);
        }

        public void RemoveEffect(StatusEffect effect)
        {
            _effects.Remove(effect);
        }

        public int ModifyDamage(int damage)
        {
            foreach (var effect in _effects)
            {
                damage = effect.ModifyDamage(damage);
            }

            return damage;
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