using System;
using UnityEngine.SceneManagement;
using Zenject;
using Duels.UI;
using Duels.Units;

namespace Duels.Core
{
    public class GameRestarter : IInitializable, IDisposable
    {
        private readonly RestartView _restartView;
        private readonly BattleEvents _battleEvents;

        public GameRestarter(RestartView restartView, BattleEvents battleEvents)
        {
            _restartView = restartView;
            _battleEvents = battleEvents;
        }

        public void Initialize()
        {
            _battleEvents.WinnerDeclared += OnWinnerDeclared;
            _restartView.RestartButton.onClick.AddListener(RestartGame);
        }

        private void OnWinnerDeclared(Unit winner, Unit loser)
        {
            _restartView.ShowRestart();
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Dispose()
        {
            _restartView.RestartButton.onClick.RemoveListener(RestartGame);
            _battleEvents.WinnerDeclared -= OnWinnerDeclared;
        }
    }
}