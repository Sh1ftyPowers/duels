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

        public Unit SpawnTeamOne()
        {
            int teamOneLength = _spawnConfig.CountTeamOnePrefabs();

            Unit prefab = _spawnConfig.TeamOnePrefabs[Random.Range(0, teamOneLength)];

            return Spawn(prefab, _spawnPoints.TeamOne);
        }

        public Unit SpawnTeamTwo()
        {
            int teamTwoLength = _spawnConfig.CountTeamTwoPrefabs();

            Unit prefab = _spawnConfig.TeamTwoPrefabs[Random.Range(0, teamTwoLength)];

            return Spawn(prefab, _spawnPoints.TeamTwo);
        }
    }
}