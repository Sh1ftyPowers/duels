using UnityEngine;
using Duels.UI;
using Duels.Units;

namespace Duels.Core
{
    public class VictoryHandler
    {
        private BattleUI _battleUI;
        private GameObject _gameOverCanvas;

        public VictoryHandler(BattleUI battleUI, GameObject gameOverCanvas)
        {
            _battleUI = battleUI;
            _gameOverCanvas = gameOverCanvas;
        }

        public bool CheckVictory(Unit attacker, Unit defender)
        {
            if (defender.CurrentHealthPoints > 0)
                return false;

            attacker.UnitAnimationManager.PlayVictoryAnimation();
            defender.UnitAnimationManager.PlayDeathAnimation();

            _battleUI.SetTurnText(attacker.UnitName + " killed " + defender.UnitName + "!");
            _battleUI.SetStatusText("Glory to the Winner!");

            _gameOverCanvas.SetActive(true);

            return true;
        }
    }
}