using Cysharp.Threading.Tasks;
using Duels.Effects;
using Duels.Presentation;
using Duels.Units;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;

namespace Duels.Core
{
    public class TurnHandler
    {
        private readonly EffectsManager _effects;
        private readonly VictoryHandler _victoryHandler;
        private readonly BattlePresenter _battlePresenter;

        private const int AttackDelay = 3000;

        public TurnHandler(EffectsManager effects, VictoryHandler victoryHandler, BattlePresenter battlePresenter)
        {
            _effects = effects;
            _victoryHandler = victoryHandler;
            _battlePresenter = battlePresenter;

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
            _battlePresenter.ShowTurn(attacker);
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
            if (_victoryHandler.IsDead(defender))
            {
                await _victoryHandler.HandleVictory(attacker, defender, cancellationToken);
                return true;
            }

            if (_victoryHandler.IsDead(attacker))
            {
                await _victoryHandler.HandleVictory(defender, attacker, cancellationToken);
                return true;
            }

            return false;
        }

        private void OnEffectApplied(Unit unit, StatusEffect effect)
        {
            _battlePresenter.ShowEffectApplied(unit, effect);
        }

        private void OnEffectExpired(Unit unit, StatusEffect effect)
        {
            _battlePresenter.ShowEffectExpired(unit, effect);
        }

        public void Dispose()
        {
            _effects.EffectApplied -= OnEffectApplied;
            _effects.EffectExpired -= OnEffectExpired;
        }
    }
}