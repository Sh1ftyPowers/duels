using UnityEngine;

public class WarriorAttack : BaseAttack
{
    public override void AttackEnemy(Unit attacker, Unit target)
    {
        attacker.animator.SetTrigger("attack");
        target.TakeDamage(attacker.damage);

        if (UnityEngine.Random.value < 0.3f)
        {
            //attacker.animator.SetTrigger("secondAttack"); // Было бы круто для стан атаки подключить своб анимацию
            target.ApplyEffect(new StunningAttack(5));
        }
    }
}
