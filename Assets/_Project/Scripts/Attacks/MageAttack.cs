public class MageAttack : BaseAttack
{
    public override AttackResult AttackEnemy(Unit attacker, Unit target)
    {
        attacker.PlayAttackAnimation();

        target.TakeDamage(attacker.Damage);

        StatusEffect effect = null;

        if (UnityEngine.Random.value < 0.3f)
        {
            effect = new WeakeningAttack(5);
        }

        return new AttackResult
        {
            Damage = attacker.Damage,
            Effect = effect
        };
    }
}
