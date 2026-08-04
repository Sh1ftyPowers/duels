using System;
using System.Threading;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using Duels.Core;

namespace Duels.Audio
{
    public class AudioManager : IInitializable, IDisposable
    {
        private readonly BattleEvents _battleEvents;

        private readonly AudioSource _musicSource;

        private AudioClip _battleTheme;
        private AudioClip _victorySound;
        private AudioClip _restartTheme;

        private readonly CancellationToken _token;

        private const int MillisecondsPerSecond = 1000;
        private const int DelayBetweenVictorySoundAndRestartTheme = 500;

        public AudioManager(AudioSource musicSource, AudioConfig audioConfig, BattleEvents battleEvents, CancellationToken token)
        {

            _musicSource = musicSource;

            _battleTheme = audioConfig.BattleTheme;
            _victorySound = audioConfig.VictorySound;
            _restartTheme = audioConfig.RestartTheme;

            _battleEvents = battleEvents;

            _token = token;
        }

        public void Initialize()
        {
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

            _musicSource.clip = _restartTheme;
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