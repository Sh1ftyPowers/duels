using System;
using Zenject;
using Duels.Core;
using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Presentation
{
    public class BattlePresenter : IInitializable, IDisposable
    {
        private readonly BattleView _battleView;

        private readonly MessageSystem _messageSystem;

        private readonly EffectsManager _effects;

        private readonly TurnHandler _turnHandler;

        private readonly BattleEvents _battleEvents;

        public BattlePresenter(BattleView battleView, MessageSystem messageSystem, EffectsManager effects, TurnHandler turnHandler, BattleEvents battleEvents)
        {
            _battleView = battleView;
            _effects = effects;
            _messageSystem = messageSystem;
            _turnHandler = turnHandler;
            _battleEvents = battleEvents;
        }

        public void Initialize()
        {
            _messageSystem.MessageAvailable += OnMessageAvailable;

            _effects.EffectApplied += OnEffectApplied;
            _effects.EffectExpired += OnEffectExpired;

            _turnHandler.TurnStarted += OnTurnStarted;

            _battleEvents.BattleStarted += OnBattleStarted;

            _battleEvents.WinnerDeclared += OnWinnerDeclared;
        }

        private void SetTurnText(string text)
        {
            _battleView.SetTurnText(text);
        }

        private void ShowTurn(Unit attacker)
        {
            SetTurnText($"{attacker.UnitName} attacks!");
        }

        private void ShowBattleStart()
        {
            SetTurnText("The Battle Begins!");
        }

        private void AnnounceTheWinner(Unit winner)
        {
            SetTurnText($"The winner is {winner.UnitName}");
        }
        private void PraiseTheWinner()
        {
            _messageSystem.ShowMessageText("Glory to the Winner!");
        }

        private void OnMessageAvailable(string message)
        {
            _battleView.SetStatusText(message);
        }

        private void OnEffectApplied(Unit unit, StatusEffect effect)
        {
            _messageSystem.ShowMessageText($"{unit.UnitName} is {effect.EffectName}");
        }

        private void OnEffectExpired(Unit unit, StatusEffect effect)
        {
            _messageSystem.ShowMessageText($"{unit.UnitName} is no longer {effect.EffectName}");
        }

        private void OnTurnStarted(Unit attacker)
        {
            ShowTurn(attacker);
        }

        private void OnWinnerDeclared(Unit winner)
        {
            AnnounceTheWinner(winner);
            PraiseTheWinner();
        }

        private void OnBattleStarted()
        {
            _battleView.ResetBattleUI();
            _battleView.ShowBattleUI();

            ShowBattleStart();
        }

        public void Dispose()
        {
            _messageSystem.MessageAvailable -= OnMessageAvailable;

            _effects.EffectApplied -= OnEffectApplied;
            _effects.EffectExpired -= OnEffectExpired;

            _turnHandler.TurnStarted -= OnTurnStarted;

            _battleEvents.BattleStarted -= OnBattleStarted;

            _battleEvents.WinnerDeclared -= OnWinnerDeclared;
        }
    }
}