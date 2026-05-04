using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _teamOnePrefabs;
    [SerializeField] private GameObject[] _teamTwoPrefabs;

    [SerializeField] private Transform _teamOneSpawnPoint;
    [SerializeField] private Transform _teamTwoSpawnPoint;

    public Unit Spawn(GameObject prefab, Transform point, BattleSystem system, MessageSystem messages)
    {
        GameObject obj = Instantiate(prefab, point);
        Unit unit = obj.GetComponent<Unit>();
        return unit;
    }

    public Unit SpawnTeamOne(GameObject prefab, BattleSystem system, MessageSystem messages)
    {
        prefab = _teamOnePrefabs[Random.Range(0, _teamOnePrefabs.Length)];
        return Spawn(prefab, _teamOneSpawnPoint, system, messages);
    }

    public Unit SpawnTeamTwo(GameObject prefab, BattleSystem system, MessageSystem messages)
    {
        prefab = _teamTwoPrefabs[Random.Range(0, _teamOnePrefabs.Length)];
        return Spawn(prefab, _teamTwoSpawnPoint, system, messages);
    }
}