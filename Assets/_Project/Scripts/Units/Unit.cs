using System;
using System.Collections.Generic;
using UnityEngine;
using Duels.Attacks;
using Duels.Effects;

namespace Duels.Units
{
    public class Unit : MonoBehaviour
    {
        [field: SerializeField] public BaseAttack BaseAttack { get; private set; }

        [SerializeField] private UnitAnimationManager _unitAnimationManager;

        [SerializeField] private UnitConfig _config;

        private readonly DamageCalculator _damageCalculator = new();

        private readonly EffectsHolder _effects = new();

        public UnitType UnitType => _config.UnitType;
        public string UnitName => _config.Name;
        public int Damage => _config.Damage;
        public int MaxHealthPoints => _config.MaxHealthPoints;
        public int CurrentHealthPoints { get; private set; }

        public event Action<Unit> HealthChanged;

        public void Initialize()
        {
            CurrentHealthPoints = MaxHealthPoints;

            HealthChanged?.Invoke(this);
        }

        public AttackResult PerformAttack(Unit target)
        {
            AttackResult result = BaseAttack.AttackEnemy(this, target);

            int damageDealt = _damageCalculator.ApplyTypeAdvantage(this, target, result.Damage);
                
            damageDealt = _effects.ModifyDamage(damageDealt);

            result.Damage = damageDealt;

            target.TakeDamage(damageDealt);

            return result;
        }

        public void TakeDamage(int damage)
        {
            damage = Mathf.Max(0, damage);
            
            CurrentHealthPoints = Mathf.Max(0, CurrentHealthPoints - damage);

            Debug.Log($"{UnitName} получил урон {damage}");

            HealthChanged?.Invoke(this);
        }

        public void TakePoisonDamage(int poisonDamage)
        {
            TakeDamage(poisonDamage);
        }

        public bool CanAct()
        {
            return !_effects.HasEffect<StunningAttack>();
        }

        public void AddEffect(StatusEffect effect)
        {
            _effects.AddEffect(effect);
        }

        public void RemoveEffect(StatusEffect effect)
        {
            _effects.RemoveEffect(effect);
        }

        public IReadOnlyList<StatusEffect> ActiveEffects => _effects.ActiveEffects;

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
    }
}