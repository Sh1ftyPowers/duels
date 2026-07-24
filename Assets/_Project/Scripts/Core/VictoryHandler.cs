using System.Threading;
using Cysharp.Threading.Tasks;
using Duels.Audio;
using Duels.Presentation;
using Duels.Units;

namespace Duels.Core
{
    public class VictoryHandler
    {
        private readonly BattlePresenter _battlePresenter;
        private readonly AudioManager _audio;

        public VictoryHandler(BattlePresenter battlePresenter, AudioManager audio)
        {
            _battlePresenter = battlePresenter;
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
            _battlePresenter.ShowRestartCanvas();
        }
    }
}