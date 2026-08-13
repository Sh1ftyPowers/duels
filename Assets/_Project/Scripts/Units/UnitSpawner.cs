using UnityEngine;

namespace Duels.Units
{
    public class UnitSpawner
    {
        private readonly UnitSpawnConfig _spawnConfig;
        private readonly SpawnPoints _spawnPoints;

        public UnitSpawner(UnitSpawnConfig spawnConfig, SpawnPoints spawnPoints)
        {
            _spawnConfig = spawnConfig;
            _spawnPoints = spawnPoints;
        }
        
        private Unit Spawn(Unit prefab, Transform point)
        {
            Unit unit = Object.Instantiate(prefab, point);

            return unit;
        }

        public Unit SpawnEnemyTeamHero()
        {
            int enemyTeamLength = _spawnConfig.CountEnemyTeamPrefabs();

            Unit enemyHeroPrefab = _spawnConfig.EnemyTeamPrefabs[Random.Range(0, enemyTeamLength)];

            return Spawn(enemyHeroPrefab, _spawnPoints.EnemyTeamSpawnPoint);
        }

        public Unit SpawnPlayerTeamHero()
        {
            int playerTeamLength = _spawnConfig.CountPlayerTeamPrefabs();

            Unit playerHeroPrefab = _spawnConfig.PlayerTeamPrefabs[Random.Range(0, playerTeamLength)];

            return Spawn(playerHeroPrefab, _spawnPoints.PlayerTeamSpawnPoint);
        }
    }
}