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

        public bool IsDead(Unit unit)
        {
            return unit.CurrentHealthPoints <= 0;
        }

        public async UniTask HandleVictory(Unit winner, Unit loser, CancellationToken cancellationToken)
        {
            winner.PlayVictoryAnimation();
            loser.PlayDeathAnimation();

            _battlePresenter.AnnounceTheWinner(winner, loser);
            _battlePresenter.PraiseTheWinner();

            await _audio.PlayEndBattleMusic(cancellationToken);
            _gameOverCanvas.SetActive(true);
        }
    }
}