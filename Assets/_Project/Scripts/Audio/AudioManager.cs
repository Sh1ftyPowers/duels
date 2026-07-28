using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Duels.Audio
{
    public class AudioManager
    {
        private readonly AudioSource _musicSource;
        private readonly AudioClip _battleTheme;
        private readonly AudioClip _victorySound;
        private readonly AudioClip _restartMenuTheme;

        private const int MillisecondsPerSecond = 1000;
        private const int DelayBetweenVictorySoundAndRestartTheme = 500;

        public AudioManager(AudioSource musicSource, AudioClip battleTheme, AudioClip victorySound, AudioClip restartMenuTheme)
        {
            _musicSource = musicSource;
            _battleTheme = battleTheme;
            _victorySound = victorySound;
            _restartMenuTheme = restartMenuTheme;
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
    }
}