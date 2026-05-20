using Duels.Units;

namespace Duels.Effects
{
    public abstract class StatusEffect
    {
        public int Duration { get; protected set; }
        public string EffectName { get; protected set; }

        public abstract void Apply(Unit target);
        public virtual void OnTurnStart(Unit target) { }
        public virtual void Remove(Unit target) { }
    }
}