public abstract class StatusEffect
{
    public int Duration;
    public string StatusName;

    public abstract void Apply(Unit target, MessageSystem message);
    public virtual void OnTurnStart(Unit target) { }
    public virtual void Remove(Unit target) { }
}