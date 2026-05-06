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

        public List<StatusEffect> GetEffects()
        {
            return _effects;
        }
    }
}
