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
        private readonly MessageSystem _message;
        private readonly EffectsManager _effects;
        private readonly VictoryHandler _victoryHandler;

        private const int AttackDelay = 3000;

        public TurnHandler(BattleUI battleUI, MessageSystem message, EffectsManager effects, VictoryHandler victoryHandler)
        {
            _battleUI = battleUI;
            _message = message;
            _effects = effects;
            _victoryHandler = victoryHandler;
        }

        public async UniTask<bool> HandleTurn(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            ShowTurnText(attacker);

            await WaitForNewMessage(cancellationToken);

            ProcessTurnStart(attacker, defender);

            await WaitForNewMessage(cancellationToken);

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

            await _message.WaitForMessages(cancellationToken);

            if (result.Effect != null)
                _effects.ApplyEffect(defender, result.Effect);

            await _message.WaitForMessages(cancellationToken);
        }

        private async UniTask<bool> TryHandleVictory(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            if (!_victoryHandler.IsVictory(defender))
                return false;

            await _victoryHandler.HandleVictory(attacker, defender, cancellationToken);

            return true;
        }

        private async UniTask WaitForNewMessage(CancellationToken cancellationToken)
        {
            await _message.WaitForMessages(cancellationToken);
        }
    }
}