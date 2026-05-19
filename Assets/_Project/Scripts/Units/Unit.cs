using System.Collections.Generic;
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

        [field:SerializeField] public int DamageReduction { get; private set; }

        [SerializeField] private BaseAttack _baseAttack;
        public BaseAttack BaseAttack => _baseAttack;

        private EffectsHolder _effects = new EffectsHolder();
        public EffectsHolder Effects => _effects;

        [SerializeField] private UnitAnimationManager _unitAnimationManager;

        [SerializeField] private Healthbar _healthbar;

        public bool IsStunned { get; private set; }
        public bool IsWeakened { get; private set; }
        public bool IsPoisoned { get; private set; }

        private void Start()
        {
            MaxHealthPoints = CurrentHealthPoints;

            _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
        }

        public AttackResult PerformAttack(Unit target)
        {
            AttackResult result = _baseAttack.AttackEnemy(this, target);

            int damageDealt = result.Damage;

            if (IsWeakened)
            {
                damageDealt -= DamageReduction;
            }

            Debug.Log(target.UnitName + " получил урон: " + damageDealt);

            return result;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealthPoints -= damage;

            _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
        }

        public void TakePoisonDamage(int poisonDamage)
        {
            CurrentHealthPoints -= poisonDamage;

            _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
        }

        public void ApplyPoison()
        {
            IsPoisoned = true;

        }

        public void RemovePoison()
        {
            IsPoisoned = false;
        }

        public void ApplyWeakness(int damageReductionValue)
        {
            IsWeakened = true;
            DamageReduction = damageReductionValue;
        }

        public void RemoveWeakness()
        {
            IsWeakened = false;
            DamageReduction = 0;
        }
        public void ApplyStun()
        {
            IsStunned = true;
        }

        public void RemoveStun()
        {
            IsStunned = false;
        }

        public void PlayAttackAnimation()
        {
            _unitAnimationManager.PlayAttackAnimation();
        }

        public void PlayDeathAnimation()
        {
            _unitAnimationManager.PlayDeathAnimation();
        }

        public void PlayVictoryAnimation()
        {
            _unitAnimationManager.PlayVictoryAnimation();
        }

        public void PlayStunAnimation()
        {
            _unitAnimationManager.PlayStunAnimation();
        }

        public void AddEffect(StatusEffect effect)
        {
            _effects.AddEffect(effect);
        }

        public List<StatusEffect> GetEffects()
        {
            return _effects.GetEffects();
        }
    }
}
