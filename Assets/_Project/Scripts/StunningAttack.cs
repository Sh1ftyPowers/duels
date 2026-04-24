using UnityEngine;

public class StunningAttack : StatusEffect
{
    public new string statusName = "stunned";

    public StunningAttack(int duration)
    {
        duration = 1;
    }

    public override void Apply(Unit target)
    {
        target.isStunned = true;
        //target.animator.SetTrigger("isStunned");
        target.PlayStunAnimation();

        target.Log(target.unitName + " is stunned!");
    }

    public override void OnTurnStart(Unit target)
    {
        duration--;
    }

    public override void Remove(Unit target)
    {
        target.isStunned = false;
    }
}
