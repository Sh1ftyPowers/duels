public class WeakeningAttack : StatusEffect
{
    public new string StatusName = "weakened";

    private int _reduction;

    public WeakeningAttack(int reduction)
    {
        this._reduction = reduction;
        Duration = 1;
    }

    public override void Apply(Unit target, MessageSystem message)
    {
        target.IsWeakened = true;
        target.DamageReduction = -_reduction;

        message.ShowMessageText(target.UnitName + " is weakened!");
    }

    public override void OnTurnStart(Unit target)
    {
        Duration--;
    }

    public override void Remove(Unit target)
    {
        target.IsWeakened = false;
    }
}
