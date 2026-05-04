public class WarriorAttack : BaseAttack
{
    public override AttackResult AttackEnemy(Unit attacker, Unit target)
    {
        attacker.PlayAttackAnimation();
        target.TakeDamage(attacker.Damage);

        StatusEffect effect = null;

        if (UnityEngine.Random.value < 0.3f)
        {
            effect = new StunningAttack();
        }

        return new AttackResult
        {
            Damage = attacker.Damage,
            Effect = effect
        };
    }
}
