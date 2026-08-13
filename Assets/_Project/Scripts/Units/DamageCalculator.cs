using UnityEngine;

namespace Duels.Units
{
    public class DamageCalculator
    {
        private const float AdvantageMultiplier = 1.75f;

        public int ApplyTypeAdvantage(Unit attacker, Unit defender, int baseDamage)
        {
            if (HasAdvantage(attacker.UnitType, defender.UnitType))
                return Mathf.RoundToInt(baseDamage * AdvantageMultiplier);

            return baseDamage;
        }

        private bool HasAdvantage(UnitType attacker, UnitType defender)
        {
            return attacker == UnitType.Warrior && defender == UnitType.Archer
                || attacker == UnitType.Archer && defender == UnitType.Mage
                || attacker == UnitType.Mage && defender == UnitType.Warrior;
        }
    }
}