using UnityEngine;
using Duels.Attacks;
using Duels.Effects;
using Duels.UI;

namespace Duels.Units
{
    public class Unit : MonoBehaviour
    {
        [field:SerializeField] public string UnitName { get; private set; }

        [field:SerializeField] public int Damage { get; private set; }

        [field:SerializeField] public int MaxHealthPoints { get; private set; }
        [field:SerializeField] public int CurrentHealthPoints { get; private set; }

        [field:SerializeField] public int UnitID { get; private set; }

        [field: SerializeField] public BaseAttack BaseAttack { get; private set; }

        [field:SerializeField] public UnitAnimationManager UnitAnimationManager { get; private set; }

        public EffectsHolder Effects { get; } = new();

        [SerializeField] private Healthbar _healthbar;

        private void Start()
        {
            MaxHealthPoints = CurrentHealthPoints;

            _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
        }

        public AttackResult PerformAttack(Unit target)
        {
            AttackResult result = BaseAttack.AttackEnemy(this, target);

            int damageDealt = Effects.ModifyDamage(result.Damage);

            result.Damage = damageDealt;

            target.TakeDamage(damageDealt);

            return result;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealthPoints -= damage;

            Debug.Log($"{UnitName} получил урон {damage}");

            _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
        }

        public void TakePoisonDamage(int poisonDamage)
        {
            CurrentHealthPoints -= poisonDamage;

            _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
        }

        public bool CanAct()
        {
            return !Effects.HasEffect<StunningAttack>();
        }
    }
}