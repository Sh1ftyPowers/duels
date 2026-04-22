using System.Collections;
using UnityEngine;

public class PoisonedArrows : StatusEffect
{
    public new string statusName = "poisoned";
    
    private int poisonDamagePerTick;
    private float tickInterval = 1f;
    private float duration = 5f;

    public PoisonedArrows(int damage)
    {
        poisonDamagePerTick = damage;
    }

    public override void Apply(Unit target)
    {
        target.Log(target.unitName + " is poisoned!");
        target.StartCoroutine(PoisonedArrowsCoroutine(target));
    }

    private IEnumerator PoisonedArrowsCoroutine(Unit target)
    {
        float timer = 0f;

        while ( timer < duration )
        {
            target.TakePoisonDamage(poisonDamagePerTick);

            yield return new WaitForSecondsRealtime(tickInterval);  // Можно поменять на WaitForSeconds()
            timer += tickInterval;
        }
    }

    public override void OnTurnStart(Unit target) { }
    public override void Remove(Unit target) { }
}
