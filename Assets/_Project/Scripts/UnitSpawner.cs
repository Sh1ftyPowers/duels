using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]  private Transform _teamOneSpawnPoint;
    [SerializeField]  private Transform _teamTwoSpawnPoint;

    public Unit Spawn(GameObject prefab, Transform point, BattleSystem system, MessageSystem messages)
    {
        GameObject obj = Instantiate(prefab, point);
        Unit unit = obj.GetComponent<Unit>();
        unit.Init(/*system,*/ messages);
        return unit;
    }

    public Unit SpawnTeamOne(GameObject prefab, BattleSystem system, MessageSystem messages)
    {
        return Spawn(prefab, _teamOneSpawnPoint, system, messages);
    }

    public Unit SpawnTeamTwo(GameObject prefab, BattleSystem system, MessageSystem messages)
    {
        return Spawn(prefab, _teamTwoSpawnPoint, system, messages);
    }
}

