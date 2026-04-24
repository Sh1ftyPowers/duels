using UnityEngine;

public class WarriorAttack : BaseAttack
{
    public override AttackResult AttackEnemy(Unit attacker, Unit target)
    {
        attacker.PlayAttackAnimation();
        target.TakeDamage(attacker.damage);

        StatusEffect effect = null;

        if (UnityEngine.Random.value < 0.3f)
        {
            //attacker.animator.SetTrigger("secondAttack"); // Было бы круто для стан атаки подключить своб анимацию
            effect = new StunningAttack();
        }

        return new AttackResult
        {
            Damage = attacker.damage,
            Effect = effect
        };
    }
}
