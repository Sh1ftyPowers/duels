using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private MessageSystem _message;

    public void ApplyEffect(Unit unit, StatusEffect effect)
    {
        unit.AddEffect(effect);
        effect.Apply(unit, _message);
    }

    public void ProcessEffects(Unit unit)
    {
        if (unit.EffectsList.Count == 0)
            return;

        foreach (var effect in unit.EffectsList.ToList())
        {
            effect.OnTurnStart(unit);

            if (effect.Duration <= 0)
            {
                effect.Remove(unit);
                unit.EffectsList.Remove(effect);

                _message.ShowMessageText($"{unit.UnitName} lost an effect");
            }
        }

        if (unit.EffectsList.Count == 0)
        {
            _message.ShowMessageText($"{unit.UnitName} has no active effects");
        }
    }
}
