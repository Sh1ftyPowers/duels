using System;
using Zenject;
using Duels.Core;

namespace Duels.Save
{
    public class GameSaveController : IInitializable, IDisposable
    {
        private readonly PlayerDataService _playerDataService;
        private readonly GameExiter _gameExiter;
        private readonly PlayerProgressEvents _playerProgressEvents;

        public GameSaveController(PlayerDataService playerDataService, GameExiter gameExiter, PlayerProgressEvents playerProgressEvents)
        {
            _playerDataService = playerDataService;
            _gameExiter = gameExiter;
            _playerProgressEvents = playerProgressEvents;
        }

        public void Initialize()
        {
            _playerProgressEvents.ProgressChanged  += OnProgressChanged;
            _gameExiter.ExitRequested += OnExitRequested;
        }

        private void OnProgressChanged()
        {
            _playerDataService.Save();
        }

        private void OnExitRequested()
        {
            _playerDataService.Save();
        }

        public void Dispose()
        {
            _playerProgressEvents.ProgressChanged -= OnProgressChanged;
            _gameExiter.ExitRequested -= OnExitRequested;
        }
    }
}