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

    [SerializeField] public Animator animator;

    [SerializeField] private Healthbar _healthbar;

    [SerializeField] private int _unitID;

    //[SerializeField] private BaseAttack _attack;

    public bool isStunned = false;
    public bool isWeakened = false;

    public int damageReduction = -5;

    public BaseAttack attack;

    public List<StatusEffect> effects = new List<StatusEffect>();

    private BattleSystem _battleSystem;

    private bool _hadEffectsLastTurn = false;

    private MessageSystem _messageSystem;

    private void Start()
    {
        maxHealthPoints = currentHealthPoints;

        _healthbar.UpdateHealthBar(currentHealthPoints, maxHealthPoints);
    }

    public void Init(BattleSystem battleSystem, MessageSystem messages)
    {
        _battleSystem = battleSystem;
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

    public void ApplyEffect(StatusEffect effect)
    {
        effects.Add(effect);
        effect.Apply(this);
    }

    public void ProcessEffects()
    {
        if (effects.Count == 0)
        {
            if (_hadEffectsLastTurn)
            {
                Log(unitName + " has no active negative effects");
            }

            _hadEffectsLastTurn = false;
            return;
        }

        _hadEffectsLastTurn = true;

        foreach (var effect in effects.ToList())
        {
            effect.OnTurnStart(this);

            if (effect.duration <= 0)
            {
                effect.Remove(this);
                Log(unitName + " no longer has any negative effects");
                effects.Remove(effect);
            }
        }
    }

    public void Log(string message)
    {
        _messageSystem.ShowMessageText(message);
    }
}
