using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public string unitName;

    [SerializeField] public int damage;

    [SerializeField] public int maxHealthPoints;
    [SerializeField] public int currentHealthPoints;

    [SerializeField] private Animator animator;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private int _unitID;

    [SerializeField] private EffectsManager _effects;

    //[SerializeField] private BaseAttack _attack;

    public bool isStunned = false;
    public bool isWeakened = false;

    public int damageReduction = -5;

    public BaseAttack attack;

    public List<StatusEffect> effects = new List<StatusEffect>();

    //private BattleSystem _battleSystem;

    private MessageSystem _messageSystem;

    private void Start()
    {
        maxHealthPoints = currentHealthPoints;

        _healthbar.UpdateHealthBar(currentHealthPoints, maxHealthPoints);
    }

    public void Init(/*BattleSystem battleSystem, */MessageSystem messages)
    {
        //_battleSystem = battleSystem;
        _messageSystem = messages;
    }

    public void TakeDamage(int damage)
    {
        currentHealthPoints -= damage;

        //animator.SetTrigger("takeDamage");

        _healthbar.UpdateHealthBar(currentHealthPoints, maxHealthPoints);
    }

    public void TakePoisonDamage(int poisonDamage)
    {
        currentHealthPoints -= poisonDamage;

        _healthbar.UpdateHealthBar(currentHealthPoints, maxHealthPoints);
    }

    public void PerformAttack(Unit target)
    {
        attack.AttackEnemy(this, target);
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("isDead");
    }

    public void PlayVictoryAnimation()
    {
        animator.SetTrigger("isWinner");
    }

    public void PlayStunAnimation()
    {
        animator.SetTrigger("isStunned");
    }

    public void AddEffect(StatusEffect effect)
    {
        effects.Add(effect);
    }

    public List<StatusEffect> GetEffects()
    {
        return effects;
    }

    public void Log(string message)
    {
        _messageSystem.ShowMessageText(message);
    }
}
