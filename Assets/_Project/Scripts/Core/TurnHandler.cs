using System;
using System.Threading;
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

        private const int AttackDelay = 3000;

        public TurnHandler(EffectsManager effects, VictoryHandler victoryHandler)
        {
            _effects = effects;
            _victoryHandler = victoryHandler;
        }

        public async UniTask<bool> HandleTurn(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            ShowTurnText(attacker);

            ProcessTurnStart(attacker, defender);

            if (TryHandleVictory(attacker, defender))
                return true;

            if (!attacker.CanAct())
                return false;

            await AttackTheEnemy(attacker, defender, cancellationToken);

            return TryHandleVictory(attacker, defender);
        }

        private void ShowTurnText(Unit attacker)
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
            await UniTask.Delay(AttackDelay, cancellationToken: cancellationToken);

            var result = attacker.PerformAttack(defender);

            if (result.Effect != null)
                _effects.ApplyEffect(defender, result.Effect);
        }

        private bool TryHandleVictory(Unit attacker, Unit defender)
        {
            if (_victoryHandler.IsDead(attacker))
            {
                _victoryHandler.HandleVictory(defender, attacker);
                return true;
            }

            if (_victoryHandler.IsDead(defender))
            {
                _victoryHandler.HandleVictory(attacker, defender);
                return true;
            }

            return false;
        }
    }
}