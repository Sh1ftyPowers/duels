using UnityEngine;
using Duels.Units;

namespace Duels.Attacks
{
    public abstract class BaseAttack : ScriptableObject
    {
        public abstract AttackResult AttackEnemy(Unit attacker, Unit target);
    }
}