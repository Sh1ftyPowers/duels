using Duels.UI;
using System;
using UnityEngine;
using Zenject;

namespace Duels.Core
{
    public class GameExiter : IInitializable, IDisposable
    {
        private readonly MainMenuView _view;

        public GameExiter(MainMenuView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.ExitGameButton.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            Debug.Log("Quit button pressed");
#else
            Application.Quit();
#endif
        }

        public void Dispose()
        {
            _view.ExitGameButton.onClick.RemoveListener(QuitGame);
        }
    }
}