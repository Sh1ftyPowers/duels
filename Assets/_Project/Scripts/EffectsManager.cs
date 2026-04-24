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
        if (unit.effects.Count == 0)
            return;

        foreach (var effect in unit.effects.ToList())
        {
            effect.OnTurnStart(unit);

            if (effect.duration <= 0)
            {
                effect.Remove(unit);
                unit.effects.Remove(effect);

                _message.ShowMessageText($"{unit.unitName} lost an effect");
            }
        }

        if (unit.effects.Count == 0)
        {
            _message.ShowMessageText($"{unit.unitName} has no active effects");
        }
    }
}
