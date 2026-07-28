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

        [field: SerializeField] public UnitAnimationManager UnitAnimationManager { get; private set; }

        [SerializeField] private UnitConfig _config;

        private readonly EffectsHolder _effects = new();

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

            int damageDealt = _effects.ModifyDamage(result.Damage);

            result.Damage = damageDealt;

            target.TakeDamage(damageDealt);

            return result;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealthPoints -= damage;

            Debug.Log($"{UnitName} получил урон {damage}");

            HealthChanged?.Invoke(this);
        }

        public void TakePoisonDamage(int poisonDamage)
        {
            CurrentHealthPoints -= poisonDamage;

            HealthChanged?.Invoke(this);
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
            UnitAnimationManager.PlayAttackAnimation();
        }

        public void PlayDeathAnimation()
        {
            UnitAnimationManager.PlayDeathAnimation();
        }

        public void PlayVictoryAnimation()
        {
            UnitAnimationManager.PlayVictoryAnimation();
        }

        public void PlayStunAnimation()
        {
            UnitAnimationManager.PlayStunAnimation();
        }
    }
}