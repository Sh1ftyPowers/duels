using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Duels.Core;

namespace Duels.Audio
{
    public class AudioManager : IInitializable, IDisposable
    {
        private readonly BattleEvents _battleEvents;
        private readonly AudioSource _musicSource;
        private readonly CancellationToken _token;

        private readonly AudioClip _battleTheme;
        private readonly AudioClip _victorySound;
        private readonly AudioClip _mainMenuTheme;

        private const int MillisecondsPerSecond = 1000;
        private const int DelayBeforeMainMenuTheme = 500;

        private int _musicTransitionId = 0;
        private int _transitionId = 0;

        public AudioManager(AudioSource musicSource, AudioConfig audioConfig, BattleEvents battleEvents, CancellationToken token)
        {
            _musicSource = musicSource;

            _battleTheme = audioConfig.BattleTheme;
            _victorySound = audioConfig.VictorySound;
            _mainMenuTheme = audioConfig.MainMenuTheme;

            _battleEvents = battleEvents;
            _token = token;
        }

        public void Initialize()
        {
            _battleEvents.BattleStarted += OnBattleStarted;
            _battleEvents.BattleEnded += OnBattleEnded;

            PlayMainMenuMusic();
        }

        private void OnBattleStarted()
        {
            Debug.Log("AUDIO EVENT: BattleStarted");
            _musicTransitionId++;

            PlayBattleMusic();
        }

        private void OnBattleEnded()
        {
            Debug.Log("AUDIO EVENT: BattleEnded");
            
            _transitionId = ++_musicTransitionId;

            PlayEndBattleMusic(_token).Forget();
        }

        private void PlayBattleMusic()
        {
            Debug.Log("AUDIO: BattleTheme");
            _musicSource.clip = _battleTheme;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        private void PlayMainMenuMusic()
        {
            Debug.Log("AUDIO: MainMenuTheme");
            _musicSource.clip = _mainMenuTheme;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        private async UniTask PlayEndBattleMusic(CancellationToken cancellationToken)
        {
            _musicSource.loop = false;
            _musicSource.clip = _victorySound;
            _musicSource.Play();

            int delay = Mathf.Max(0, Mathf.RoundToInt(_victorySound.length * MillisecondsPerSecond) - DelayBeforeMainMenuTheme);

            await UniTask.Delay(delay, cancellationToken: cancellationToken);

            if (_transitionId != _musicTransitionId)
            {
                Debug.Log("AUDIO: Old menu transition cancelled");
                return;
            }

            PlayMainMenuMusic();
        }

        public void Dispose()
        {
            _battleEvents.BattleStarted -= OnBattleStarted;
            _battleEvents.BattleEnded -= OnBattleEnded;
        }
    }
}