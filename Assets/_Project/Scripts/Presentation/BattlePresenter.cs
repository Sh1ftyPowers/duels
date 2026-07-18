using System.Collections.Generic;
using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Presentation
{
    public class BattlePresenter
    {
        private readonly BattleView _battleView;

        private readonly MessageSystem _messageSystem;

        private readonly Dictionary<Unit, Healthbar> _healthbars = new();

        public BattlePresenter(BattleView battleView, MessageSystem messageSystem)
        {
            _battleView = battleView;
            _messageSystem = messageSystem;

            _messageSystem.MessageReady += OnMessageReady;
        }

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

        private void OnMessageReady(string message)
        {
            _battleView.SetStatusText(message);
        }

        public void SetTurnText(string text)
        {
            _battleView.SetTurnText(text);
        }

        public void SetStatusText(string text)
        {
            _battleView.SetStatusText(text);
        }

        public void ShowTurn(Unit attacker)
        {
            SetTurnText($"{attacker.UnitName} attacks!");
        }

        public void ShowBattleStart()
        {
            SetTurnText("The Battle Begins!");
        }

        public void ShowEffectApplied(Unit unit, StatusEffect effect)
        {
            SetStatusText($"{unit.UnitName} is {effect.EffectName}");
        }

        public void ShowEffectExpired(Unit unit, StatusEffect effect)
        {
            SetStatusText($"{unit.UnitName} is no longer {effect.EffectName}");
        }

        public void AnnounceTheWinner(Unit winner, Unit loser)
        {
            SetStatusText($"{winner.UnitName} defeated {loser.UnitName}");
        }

        public void PraiseTheWinner()
        {
            SetStatusText("Glory to the Winner!");
        }

        public void Dispose()
        {
            _messageSystem.MessageReady -= OnMessageReady;

            foreach (var unit in _healthbars.Keys)
            {
                unit.HealthChanged -= OnHealthChanged;
            }

            _healthbars.Clear();
        }
    }
}