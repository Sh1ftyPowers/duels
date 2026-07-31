using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Duels.Core
{
    public class GameRestarter : IDisposable
    {
        private readonly Button _restartButton;

        public GameRestarter(Button restartButton)
        {
            _restartButton = restartButton;

            _restartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Dispose()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
        }
    }
}