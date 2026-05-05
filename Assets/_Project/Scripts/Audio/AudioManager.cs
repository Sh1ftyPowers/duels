using UnityEngine;
using System.Collections;

namespace Duels.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioClip _battleTheme;
        [SerializeField] private AudioClip _victorySound;
        [SerializeField] private AudioClip _restartMenuTheme;

        private float _delayBetweenVictorySoundAndRestartTheme = 0.5f;

        public void PlayBattleMusic()
        {
            _musicSource.clip = _battleTheme;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        public IEnumerator PlayEndBattleMusic()
        {
            _musicSource.loop = false;
            _musicSource.clip = _victorySound;
            _musicSource.Play();

            yield return new WaitForSeconds(_victorySound.length - _delayBetweenVictorySoundAndRestartTheme);

            _musicSource.clip = _restartMenuTheme;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }
}
