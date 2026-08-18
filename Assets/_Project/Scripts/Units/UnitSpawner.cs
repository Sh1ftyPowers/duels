using UnityEngine;
using System.Collections.Generic;

namespace Duels.Units
{
    public class UnitSpawner
    {
        private readonly UnitSpawnConfig _spawnConfig;
        private readonly SpawnPoints _spawnPoints;

        private readonly List<Unit> _spawnedUnits = new();

        public UnitSpawner(UnitSpawnConfig spawnConfig, SpawnPoints spawnPoints)
        {
            _spawnConfig = spawnConfig;
            _spawnPoints = spawnPoints;
        }

        public Unit SpawnEnemyTeamHero()
        {
            Unit enemyHeroPrefab = GetRandomPrefab(_spawnConfig.EnemyTeamPrefabs);

            return Spawn(enemyHeroPrefab, _spawnPoints.EnemyTeamSpawnPoint);
        }

        public Unit SpawnPlayerTeamHero()
        {
            Unit playerHeroPrefab = GetRandomPrefab(_spawnConfig.PlayerTeamPrefabs);

            return Spawn(playerHeroPrefab, _spawnPoints.PlayerTeamSpawnPoint);
        }

        private Unit Spawn(Unit prefab, Transform point)
        {
            Unit unit = Object.Instantiate(prefab, point);

            _spawnedUnits.Add(unit);

            return unit;
        }

        private Unit GetRandomPrefab(Unit[] prefabs)
        {
            return prefabs[Random.Range(0, prefabs.Length)];
        }

        public void ClearUnits()
        {
            foreach (Unit unit in _spawnedUnits)
            {
                if (unit != null)
                    Object.Destroy(unit.gameObject);
            }

            _spawnedUnits.Clear();
        }
    }
}