using System;
using Zenject;
using Duels.UI;

namespace Duels.Core
{
    public class GameStarter : IInitializable, IDisposable
    {
        private readonly MainMenuView _mainMenuView;
        private readonly BattleView _battleView;
        private readonly BattleStarter _battleStarter;

        public GameStarter(MainMenuView view, BattleStarter battleStarter, BattleView battleView)
        {
            _mainMenuView = view;
            _battleStarter = battleStarter;
            _battleView = battleView;
        }

        public void Initialize()
        {
            _mainMenuView.StartGameButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _mainMenuView.HideMainMenuUI();

            _battleView.ShowBattleUI();

            _battleView.Reset();

            _battleStarter.Start();
        }

        public void Dispose()
        {
            _mainMenuView.StartGameButton.onClick.RemoveListener(StartGame);
        }
    }
}