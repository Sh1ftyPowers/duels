using NUnit.Framework.Constraints;
using Unity.Mathematics;
using UnityEngine;

public class ArcherAttack : BaseAttack
{
    public override AttackResult AttackEnemy(Unit attacker, Unit target)
    {
        attacker.PlayAttackAnimation();

        target.TakeDamage(attacker.damage);

        return new AttackResult
        {
            Damage = attacker.damage,
            Effect = new PoisonedArrows(2)
        };
    }
}
