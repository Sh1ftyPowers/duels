using Duels.UI;
using Duels.Units;

namespace Duels.Effects
{
    public abstract class StatusEffect
    {
        public int Duration;

        public abstract void Apply(Unit target, MessageSystem message);
        public virtual void OnTurnStart(Unit target) { }
        public virtual void Remove(Unit target) { }
    }
}