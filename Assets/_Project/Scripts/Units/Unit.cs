using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public string UnitName;

    [SerializeField] public int Damage;

    [SerializeField] public int MaxHealthPoints;
    [SerializeField] public int CurrentHealthPoints;

    [SerializeField] public int DamageReduction = -5;

    public BaseAttack Attack;

    public List<StatusEffect> EffectsList = new List<StatusEffect>();

    [SerializeField] private Animator _animator;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private int _unitID;

    public bool IsStunned = false;
    public bool IsWeakened = false;

    private void Start()
    {
        MaxHealthPoints = CurrentHealthPoints;

        _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
    }

    public void TakeDamage(int damage)
    {
        if (IsWeakened)
            damage += DamageReduction;

        CurrentHealthPoints -= damage;

        Debug.Log("Получен урон: " + damage);

        _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
    }

    public void TakePoisonDamage(int poisonDamage)
    {
        CurrentHealthPoints -= poisonDamage;

        _healthbar.UpdateHealthBar(CurrentHealthPoints, MaxHealthPoints);
    }

    public AttackResult PerformAttack(Unit target)
    {
        return Attack.AttackEnemy(this, target);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("attack");
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger("isDead");
    }

    public void PlayVictoryAnimation()
    {
        _animator.SetTrigger("isWinner");
    }

    public void PlayStunAnimation()
    {
        _animator.SetTrigger("isStunned");
    }

    public void AddEffect(StatusEffect effect)
    {
        EffectsList.Add(effect);
    }

    public List<StatusEffect> GetEffects()
    {
        return EffectsList;
    }
}
