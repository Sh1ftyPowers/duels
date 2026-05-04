public class StunningAttack : StatusEffect
{
    public new string StatusName = "stunned";

    public StunningAttack()
    {
        Duration = 1;
    }

    public override void Apply(Unit target, MessageSystem message)
    {
        target.IsStunned = true;
        target.PlayStunAnimation();

        message.ShowMessageText(target.UnitName + " is stunned!");
    }

    public override void OnTurnStart(Unit target)
    {
        Duration--;
    }

    public override void Remove(Unit target)
    {
        target.IsStunned = false;
    }
}
