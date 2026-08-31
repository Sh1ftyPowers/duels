using Zenject;

namespace Duels.Core
{
    public class SavedGameInitializer : IInitializable
    {
        private readonly PlayerDataService _playerDataService;

        public SavedGameInitializer(PlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }

        public void Initialize()
        {
            _playerDataService.Load();
        }
    }
}