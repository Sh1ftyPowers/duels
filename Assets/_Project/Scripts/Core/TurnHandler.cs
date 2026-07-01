using System.Threading;
using Cysharp.Threading.Tasks;
using Duels.Effects;
using Duels.UI;
using Duels.Units;

namespace Duels.Core
{
    public class TurnHandler
    {
        private readonly BattleUI _battleUI;
        private readonly EffectsManager _effects;
        private readonly VictoryHandler _victoryHandler;
        private readonly MessageSystem _message;

        private const int AttackDelay = 3000;

        public TurnHandler(BattleUI battleUI, EffectsManager effects, VictoryHandler victoryHandler, MessageSystem message)
        {
            _battleUI = battleUI;
            _effects = effects;
            _victoryHandler = victoryHandler;
            _message = message;

            _effects.EffectApplied += OnEffectApplied;
            _effects.EffectExpired += OnEffectExpired;
        }

        public async UniTask<bool> HandleTurn(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            ShowTurnText(attacker);

            ProcessTurnStart(attacker, defender);

            if (await TryHandleVictory(attacker, defender, cancellationToken))
                return true;

            if (!attacker.CanAct())
                return false;

            await AttackTheEnemy(attacker, defender, cancellationToken);

            return await TryHandleVictory(attacker, defender, cancellationToken);
        }

        private void ShowTurnText(Unit attacker)
        {
            _battleUI.SetTurnText($"{attacker.UnitName} attacks!");
        }

        private void ProcessTurnStart(Unit attacker, Unit defender)
        {
            _effects.ProcessEffects(attacker);
            _effects.ProcessEffects(defender);
        }

        private async UniTask AttackTheEnemy(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            await UniTask.Delay(AttackDelay, cancellationToken: cancellationToken);

            var result = attacker.PerformAttack(defender);

            if (result.Effect != null)
                _effects.ApplyEffect(defender, result.Effect);
        }

        private async UniTask<bool> TryHandleVictory(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            if (!_victoryHandler.IsVictory(defender))
                return false;

            await _victoryHandler.HandleVictory(attacker, defender, cancellationToken);

            return true;
        }

        private void OnEffectApplied(Unit unit, StatusEffect effect)
        {
            _message.ShowMessageText($"{unit.UnitName} is {effect.EffectName}");
        }

        private void OnEffectExpired(Unit unit, StatusEffect effect)
        {
            _message.ShowMessageText($"{unit.UnitName} lost {effect.EffectName}");
        }

        public void Dispose()
        {
            _effects.EffectApplied -= OnEffectApplied;
            _effects.EffectExpired -= OnEffectExpired;
        }
    }
}