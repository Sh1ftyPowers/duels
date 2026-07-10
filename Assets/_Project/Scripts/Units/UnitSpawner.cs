using UnityEngine;

namespace Duels.Units
{
    public class UnitSpawner
    {
        private readonly GameObject[] _teamOnePrefabs;
        private readonly GameObject[] _teamTwoPrefabs;

        private readonly Transform _teamOneSpawnPoint;
        private readonly Transform _teamTwoSpawnPoint;

        public UnitSpawner(GameObject[] teamOnePrefabs, GameObject[] teamTwoPrefabs, Transform teamOneSpawnPoint, Transform teamTwoSpawnPoint)
        {
            _teamOnePrefabs = teamOnePrefabs;
            _teamTwoPrefabs = teamTwoPrefabs;
            _teamOneSpawnPoint = teamOneSpawnPoint;
            _teamTwoSpawnPoint = teamTwoSpawnPoint;
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
            GameObject prefab = _teamOnePrefabs[UnityEngine.Random.Range(0, _teamOnePrefabs.Length)];
            return Spawn(prefab, _teamOneSpawnPoint);
        }

        public Unit SpawnTeamTwo()
        {
            GameObject prefab = _teamTwoPrefabs[UnityEngine.Random.Range(0, _teamTwoPrefabs.Length)];
            return Spawn(prefab, _teamTwoSpawnPoint);
        }
    }
}