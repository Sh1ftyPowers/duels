using UnityEngine;
using Duels.UI;
using Duels.Units;
using Duels.Audio;
using System.Threading.Tasks;
using System.Threading;

namespace Duels.Core
{
    public class VictoryHandler
    {
        private BattleUI _battleUI;
        private GameObject _gameOverCanvas;
        private AudioManager _audio;

        public VictoryHandler(BattleUI battleUI, GameObject gameOverCanvas, AudioManager audio)
        {
            _battleUI = battleUI;
            _gameOverCanvas = gameOverCanvas;
            _audio = audio;
        }

        public bool IsVictory(Unit defender)
        {
            return defender.CurrentHealthPoints <= 0;
        }

        public async Task HandleVictory(Unit attacker, Unit defender, CancellationToken cancellationToken)
        {
            attacker.UnitAnimationManager.PlayVictoryAnimation();
            defender.UnitAnimationManager.PlayDeathAnimation();

            _battleUI.SetTurnText(attacker.UnitName + " killed " + defender.UnitName + "!");
            _battleUI.SetStatusText("Glory to the Winner!");

            await _audio.PlayEndBattleMusic(cancellationToken);
            _gameOverCanvas.SetActive(true);
        }
    }
}