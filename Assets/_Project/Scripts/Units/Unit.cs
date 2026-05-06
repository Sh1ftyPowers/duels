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

        [SerializeField] public int DamageReduction = 0;

        public BaseAttack Attack;

        private EffectsHolder _effects = new EffectsHolder();
        public EffectsHolder Effects => _effects;

        [SerializeField] private UnitAnimationManager _unitAnimationManager;

        [SerializeField] private Healthbar _healthbar;

        public bool IsStunned;
        public bool IsWeakened;

        private void Start()
        {
            MaxHealthPoints = CurrentHealthPoints;

            _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
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

        public AttackResult PerformAttack(Unit target)
        {
            AttackResult result = Attack.AttackEnemy(this, target);

            int damageDealt = result.Damage;

            if (IsWeakened)
            {
                damageDealt -= DamageReduction;
            }

            Debug.Log(target.UnitName + " получил урон: " + damageDealt);

            return result;
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
