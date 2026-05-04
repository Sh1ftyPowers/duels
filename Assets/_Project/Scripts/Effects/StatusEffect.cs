using UnityEngine;

public abstract class StatusEffect
{
    public int Duration;
    public string StatusName;

    public abstract void Apply(Unit target, MessageSystem message);
    public abstract void OnTurnStart(Unit target);
    public abstract void Remove(Unit target);

}