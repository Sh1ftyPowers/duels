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

        private readonly RestartView _restartView;

        private readonly MessageSystem _messageSystem;

        private readonly EffectsManager _effects;

        private readonly TurnHandler _turnHandler;

        private readonly BattleEvents _battleEvents;

        public BattlePresenter(BattleView battleView, RestartView restartView, MessageSystem messageSystem, EffectsManager effects, TurnHandler turnHandler, BattleEvents battleEvents)
        {
            _battleView = battleView;
            _restartView = restartView;
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

        public void ShowBattleStart()
        {
            SetTurnText("The Battle Begins!");
        }

        private void AnnounceTheWinner(Unit winner, Unit loser)
        {
            SetTurnText($"{winner.UnitName} defeated {loser.UnitName}");
        }
        private void PraiseTheWinner()
        {
            _messageSystem.ShowMessageText("Glory to the Winner!");
        }

        private void ShowRestartCanvas()
        {
            _restartView.ShowRestart();
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

        private void OnWinnerDeclared(Unit winner, Unit loser)
        {
            AnnounceTheWinner(winner, loser);
            PraiseTheWinner();
            ShowRestartCanvas();
        }

        private void OnBattleStarted()
        {
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