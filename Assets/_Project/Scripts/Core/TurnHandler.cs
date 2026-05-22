using System.Threading;
using Cysharp.Threading.Tasks;
using Duels.Attacks;
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
            _battleUI.SetTurnText($"{attacker.UnitName} attacks!");

            await _message.WaitForMessages(cancellationToken);

            _effects.ProcessEffects(attacker);
            _effects.ProcessEffects(defender);

            await _message.WaitForMessages(cancellationToken);

            if (_victoryHandler.CheckVictory(attacker, defender))
                return true;

            if (attacker.Effects.HasEffect<StunningAttack>())
                return false;

            await UniTask.Delay(AttackDelay, cancellationToken: cancellationToken);

            var result = attacker.PerformAttack(defender);

            await _message.WaitForMessages(cancellationToken);

            if (result.Effect != null)
                _effects.ApplyEffect(defender, result.Effect);

            await _message.WaitForMessages(cancellationToken);

            return _victoryHandler.CheckVictory(attacker, defender);
        }
    }
}