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
        
        private Unit Spawn(GameObject prefab, Transform point)
        {
            GameObject obj = Object.Instantiate(prefab, point);

            if (!obj.TryGetComponent<Unit>(out Unit unit))
            {
                throw new MissingComponentException($"Prefab {prefab.name} has no Unit component");
            }

            return unit;
        }

        public Unit SpawnTeamOne()
        {
            int teamOneLenght = _spawnConfig.CountTeamOnePrefabs();

            GameObject prefab = _spawnConfig.TeamOnePrefabs[UnityEngine.Random.Range(0, teamOneLenght)];
            return Spawn(prefab, _spawnPoints.TeamOne);
        }

        public Unit SpawnTeamTwo()
        {
            int teamTwoLength = _spawnConfig.CountTeamTwoPrefabs();

            GameObject prefab = _spawnConfig.TeamTwoPrefabs[UnityEngine.Random.Range(0, teamTwoLength)];
            return Spawn(prefab, _spawnPoints.TeamTwo);
        }
    }
}