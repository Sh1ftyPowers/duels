using Duels.UI;
using System;
using UnityEngine;
using Zenject;

namespace Duels.Core
{
    public class GameExiter : IInitializable, IDisposable
    {
        private readonly MainMenuView _mainMenuView;

        public GameExiter(MainMenuView view)
        {
            _mainMenuView = view;
        }

        public void Initialize()
        {
            _mainMenuView.ExitGameButton.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            Debug.Log("Quit button pressed");
#else
            Application.Quit();
#endif
        }

        public void Dispose()
        {
            _mainMenuView.ExitGameButton.onClick.RemoveListener(ExitGame);
        }
    }
}