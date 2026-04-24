using UnityEngine;

public class StunningAttack : StatusEffect
{
    public new string statusName = "stunned";

    public StunningAttack()
    {
        duration = 1;
    }

    public override void Apply(Unit target, MessageSystem message)
    {
        target.isStunned = true;
        //target.animator.SetTrigger("isStunned");
        target.PlayStunAnimation();

        message.ShowMessageText(target.unitName + " is stunned!");
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
