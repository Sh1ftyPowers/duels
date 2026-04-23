using UnityEngine;

public class WeakeningAttack : StatusEffect
{
    public new string statusName = "weakened";

    private int reduction;

    public WeakeningAttack(int reduction)
    {
        this.reduction = reduction;
        duration = 1;
    }

    public override void Apply(Unit target)
    {
        target.isWeakened = true;
        target.damageReduction = -reduction;

        target.Log(target.unitName + " is weakened!");
    }

    public override void OnTurnStart(Unit target)
    {
        duration--;
    }

    public override void Remove(Unit target)
    {
        target.isWeakened = false;
    }
}
