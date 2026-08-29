using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Duels.Effects;
using Duels.Units;

namespace Duels.Core
{
    public class TurnHandler
    {
        private readonly EffectsManager _effects;
        private readonly VictoryHandler _victoryHandler;

        public event Action<Unit> TurnStarted;

        private const int BaseAttackDelay = 3000;

        public TurnHandler(EffectsManager effects, VictoryHandler victoryHandler)
        {
            _effects = effects;
            _victoryHandler = victoryHandler;
        }

        public async UniTask<BattleResult> HandleTurn(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            StartTurn(attacker);

            ProcessTurnStart(attacker, defender);

            var result = HandleVictoryIfNeeded(attacker, defender);

            if (result.IsFinished)
                return result;

            if (!attacker.CanAct())
                return default;

            await AttackTheEnemy(attacker, defender, cancellationToken);

            return HandleVictoryIfNeeded(attacker, defender);
        }

        private void StartTurn(Unit attacker)
        {
            TurnStarted?.Invoke(attacker);
        }

        private void ProcessTurnStart(Unit attacker, Unit defender)
        {
            _effects.ProcessEffects(attacker);
            _effects.ProcessEffects(defender);
        }

        private async UniTask AttackTheEnemy(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            int attackDelay = Mathf.RoundToInt(BaseAttackDelay / attacker.AttackSpeedMultiplier);

            await UniTask.Delay(attackDelay, cancellationToken: cancellationToken);

            var result = attacker.PerformAttack(defender);

            if (result.Effect != null)
                _effects.ApplyEffect(defender, result.Effect);
        }

        private BattleResult HandleVictoryIfNeeded(Unit attacker, Unit defender)
        {
            if (_victoryHandler.IsDead(attacker))
                return _victoryHandler.HandleVictory(defender, attacker);

            if (_victoryHandler.IsDead(defender))
                return _victoryHandler.HandleVictory(attacker, defender);

            return default;
        }
    }
}