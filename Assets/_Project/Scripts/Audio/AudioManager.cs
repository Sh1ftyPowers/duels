using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Duels.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioClip _battleTheme;
        [SerializeField] private AudioClip _victorySound;
        [SerializeField] private AudioClip _restartMenuTheme;

        private const int ConversionFactorForSecondsToMilliseconds = 1000;
        private int _delayBetweenVictorySoundAndRestartTheme = 500;

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

            await UniTask.Delay((int)_victorySound.length * ConversionFactorForSecondsToMilliseconds - _delayBetweenVictorySoundAndRestartTheme, cancellationToken: token);

            _musicSource.clip = _restartMenuTheme;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }
}
