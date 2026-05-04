using System.Collections;
using UnityEngine;

public class PoisonedArrows : StatusEffect
{
    public new string statusName = "poisoned";
    
    private int _poisonDamagePerTick;
    private float _tickInterval = 1f;
    private float _duration = 5f;

    public PoisonedArrows(int damage)
    {
        _poisonDamagePerTick = damage;
    }

    public override void Apply(Unit target, MessageSystem message)
    {
        message.ShowMessageText(target.UnitName + " is poisoned!");
        target.StartCoroutine(PoisonedArrowsCoroutine(target));
    }

    private IEnumerator PoisonedArrowsCoroutine(Unit target)
    {
        float timer = 0f;

        while ( timer < _duration )
        {
            target.TakePoisonDamage(_poisonDamagePerTick);

            yield return new WaitForSecondsRealtime(_tickInterval);  // Можно поменять на WaitForSeconds()
            timer += _tickInterval;
        }
    }

    public override void OnTurnStart(Unit target) { }
    public override void Remove(Unit target) { }
}
