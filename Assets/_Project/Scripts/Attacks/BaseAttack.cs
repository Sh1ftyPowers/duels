using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    public abstract AttackResult AttackEnemy(Unit attacker, Unit target);
}

public struct AttackResult
{
    public int Damage;
    public StatusEffect Effect;
}