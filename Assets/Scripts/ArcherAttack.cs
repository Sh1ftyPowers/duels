using Unity.Mathematics;
using UnityEngine;

public class ArcherAttack : BaseAttack
{
    public override void AttackEnemy(Unit attacker, Unit target)
    {
        attacker.animator.SetTrigger("attack");
        target.TakeDamage(attacker.damage);
        target.ApplyEffect(new PoisonedArrows(2));
    }
}
