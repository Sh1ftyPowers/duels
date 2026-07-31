using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Duels.Core;
using Duels.Units;

namespace Duels.Audio
{
    public class AudioManager
    {
        private readonly BattleEvents _battleEvents;

        private readonly AudioSource _musicSource;
        private readonly AudioClip _battleTheme;
        private readonly AudioClip _victorySound;
        private readonly AudioClip _restartMenuTheme;

        private readonly CancellationToken _token;

        private const int MillisecondsPerSecond = 1000;
        private const int DelayBetweenVictorySoundAndRestartTheme = 500;

        public AudioManager(AudioSource musicSource, AudioClip battleTheme, AudioClip victorySound, AudioClip restartMenuTheme, BattleEvents battleEvents, CancellationToken token)
        {
            _musicSource = musicSource;
            _battleTheme = battleTheme;
            _victorySound = victorySound;
            _restartMenuTheme = restartMenuTheme;

            _token = token;

            _battleEvents = battleEvents;

            _battleEvents.BattleStarted += OnBattleStarted;
            _battleEvents.BattleEnded += OnBattleEnded;
        }

        public void PlayBattleMusic()
        {
            _musicSource.clip = _battleTheme;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        public async UniTask PlayEndBattleMusic(CancellationToken token)
        {
            _musicSource.loop = false;
            _musicSource.clip = _victorySound;
            _musicSource.Play();

            int delay = Mathf.Max(0, Mathf.RoundToInt(_victorySound.length * MillisecondsPerSecond) - DelayBetweenVictorySoundAndRestartTheme);

            await UniTask.Delay(delay, cancellationToken: token);

            _musicSource.clip = _restartMenuTheme;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        private void OnBattleStarted()
        {
            PlayBattleMusic();
        }

        private void OnBattleEnded()
        {
            PlayEndBattleMusic(_token).Forget();
        }

        public void Dispose()
        {
            _battleEvents.BattleStarted -= OnBattleStarted;
            _battleEvents.BattleEnded -= OnBattleEnded;
        }
    }
}