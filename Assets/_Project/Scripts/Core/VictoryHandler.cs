using System.Threading;
using UnityEngine;
using Duels.UI;
using Duels.Units;
using Duels.Audio;
using Cysharp.Threading.Tasks;

namespace Duels.Core
{
    public class VictoryHandler
    {
        private readonly BattleView _battleView;
        private readonly GameObject _gameOverCanvas;
        private readonly AudioManager _audio;

        public VictoryHandler(BattleView battleView, GameObject gameOverCanvas, AudioManager audio)
        {
            _battleView = battleView;
            _gameOverCanvas = gameOverCanvas;
            _audio = audio;
        }

        public bool IsVictory(Unit defender)
        {
            return defender.CurrentHealthPoints <= 0;
        }

        public async UniTask HandleVictory(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            attacker.PlayVictoryAnimation();
            defender.PlayDeathAnimation();

            _battleView.SetTurnText(attacker.UnitName + " defeated " + defender.UnitName + "!");
            _battleView.SetStatusText("Glory to the Winner!");

            await _audio.PlayEndBattleMusic(cancellationToken);
            _gameOverCanvas.SetActive(true);
        }
    }
}