using System.Collections.Generic;
using Duels.UI;
using Duels.Units;

namespace Duels.Presentation
{
    public class HealthbarPresenter
    {
        private readonly Dictionary<Unit, Healthbar> _healthbars = new();

        public void RegisterUnit(Unit unit, Healthbar healthbar)
        {
            if (_healthbars.ContainsKey(unit))
                return;

            _healthbars.Add(unit, healthbar);

            unit.HealthChanged += OnHealthChanged;

            OnHealthChanged(unit);
        }

        private void OnHealthChanged(Unit unit)
        {
            _healthbars[unit].UpdateHealthBar(unit.CurrentHealthPoints, unit.MaxHealthPoints);
        }

        public void Dispose()
        {
            foreach (var unit in _healthbars.Keys)
            {
                unit.HealthChanged -= OnHealthChanged;
            }

            _healthbars.Clear();
        }
    }
}