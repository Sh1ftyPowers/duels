using UnityEngine;

namespace Duels.Units
{
    public class SpawnPoints : MonoBehaviour
    {
        [field: SerializeField] public Transform EnemyTeamSpawnPoint { get; private set; }
        [field: SerializeField] public Transform PlayerTeamSpawnPoint { get; private set; }

        /*public Transform GetSpawnPoint(TeamType team)
        {
            return team == TeamType.Player  
                ? PlayerTeamSpawnPoint
                : EnemyTeamSpawnPoint;
        }*/
    }
}