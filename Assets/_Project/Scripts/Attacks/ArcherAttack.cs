using NUnit.Framework.Constraints;
using Unity.Mathematics;
using UnityEngine;

public class ArcherAttack : BaseAttack
{
    public override AttackResult AttackEnemy(Unit attacker, Unit target)
    {
        attacker.PlayAttackAnimation();

        target.TakeDamage(attacker.Damage);

        return new AttackResult
        {
            Damage = attacker.Damage,
            Effect = new PoisonedArrows(2)
        };
    }
}
