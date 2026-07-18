using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Duels.Audio;
using Duels.Presentation;
using Duels.Units;

namespace Duels.Core
{
    public class VictoryHandler
    {
        private readonly BattlePresenter _battlePresenter;
        private readonly GameObject _gameOverCanvas;
        private readonly AudioManager _audio;

        public VictoryHandler(BattlePresenter battlePresenter, GameObject gameOverCanvas, AudioManager audio)
        {
            _battlePresenter = battlePresenter;
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

            _battlePresenter.AnnounceTheWinner(attacker, defender);
            _battlePresenter.PraiseTheWinner();

            await _audio.PlayEndBattleMusic(cancellationToken);
            _gameOverCanvas.SetActive(true);
        }
    }
}