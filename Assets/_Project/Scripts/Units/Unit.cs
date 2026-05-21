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

        [SerializeField] private BaseAttack _baseAttack;
        public BaseAttack BaseAttack => _baseAttack;

        private EffectsHolder _effects = new EffectsHolder();
        public EffectsHolder Effects => _effects;

        [SerializeField] private UnitAnimationManager _unitAnimationManager;
        public UnitAnimationManager UnitAnimationManager => _unitAnimationManager;

        [SerializeField] private Healthbar _healthbar;

        private void Start()
        {
            MaxHealthPoints = CurrentHealthPoints;

            _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
        }

        public AttackResult PerformAttack(Unit target)
        {
            AttackResult result = _baseAttack.AttackEnemy(this, target);

            int damageDealt = result.Damage;

            WeakeningAttack weakeningEffect = Effects.GetEffect<WeakeningAttack>();

            if (weakeningEffect != null)
            {
                damageDealt -= weakeningEffect.DamageReduction;
            }

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

        public void AddEffect(StatusEffect effect)
        {
            _effects.AddEffect(effect);
        }
    }
}