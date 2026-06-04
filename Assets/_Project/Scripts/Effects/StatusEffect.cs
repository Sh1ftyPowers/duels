using Duels.Units;

namespace Duels.Effects
{
    public abstract class StatusEffect
    {
        public int Duration { get; protected set; }
        public abstract string EffectName { get; }

        public abstract void Apply(Unit target);
        public virtual void OnTurnStart(Unit target) { }
        public virtual void Remove(Unit target) { }
        
        public virtual int ModifyDamage(int damage)
        {
            return damage;
        }
    }
}