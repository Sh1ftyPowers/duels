using UnityEngine;

public class MageAttack : BaseAttack
{
    public override void AttackEnemy(Unit attacker, Unit target)
    {
        attacker.animator.SetTrigger("attack");
        target.TakeDamage(attacker.damage);

        if (UnityEngine.Random.value < 0.3f)
        {
            target.ApplyEffect(new WeakeningAttack(5));
        }
    }
}
