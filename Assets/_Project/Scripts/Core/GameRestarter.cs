using System;
using UnityEngine.SceneManagement;
using Zenject;
using Duels.UI;

namespace Duels.Core
{
    public class GameRestarter : IInitializable, IDisposable
    {
        private readonly RestartView _restartView;

        public GameRestarter(RestartView restartView)
        {
            _restartView = restartView;
        }

        public void Initialize()
        {
            _restartView.RestartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Dispose()
        {
            UnityEngine.Debug.Log("GameRestarter.Dispose");
            _restartView.RestartButton.onClick.RemoveListener(RestartGame);
        }
    }
}