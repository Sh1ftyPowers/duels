using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    public abstract void AttackEnemy(Unit attacker, Unit target);
}
