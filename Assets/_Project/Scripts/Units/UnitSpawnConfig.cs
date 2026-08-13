using UnityEngine;

namespace Duels.Units
{
    [CreateAssetMenu(menuName = "Duels/Unit Spawn Config")]

    public class UnitSpawnConfig : ScriptableObject
    {
        public Unit[] EnemyTeamPrefabs;
        public Unit[] PlayerTeamPrefabs;

        public int CountEnemyTeamPrefabs()
        {
            return EnemyTeamPrefabs.Length;
        }

        public int CountPlayerTeamPrefabs()
        {
            return PlayerTeamPrefabs.Length;
        }
    }
}