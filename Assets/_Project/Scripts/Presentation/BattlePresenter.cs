using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Presentation
{
    public class BattlePresenter
    {
        private readonly BattleView _battleView;

        private readonly MessageSystem _messageSystem;

        public BattlePresenter(BattleView battleView, MessageSystem messageSystem)
        {
            _battleView = battleView;
            _messageSystem = messageSystem;

            _messageSystem.MessageAvailable += OnMessageAvailable;
        }

        private void OnMessageAvailable(string message)
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
            _messageSystem.ShowMessageText($"{unit.UnitName} is {effect.EffectName}");
        }

        public void ShowEffectExpired(Unit unit, StatusEffect effect)
        {
            _messageSystem.ShowMessageText($"{unit.UnitName} is no longer {effect.EffectName}");
        }

        public void AnnounceTheWinner(Unit winner, Unit loser)
        {
            SetTurnText($"{winner.UnitName} defeated {loser.UnitName}");
        }

        public void PraiseTheWinner()
        {
            _messageSystem.ShowMessageText("Glory to the Winner!");
        }

        public void Dispose()
        {
            _messageSystem.MessageAvailable -= OnMessageAvailable;
        }
    }
}