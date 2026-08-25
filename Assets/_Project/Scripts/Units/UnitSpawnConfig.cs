using UnityEngine;

namespace Duels.Units
{
    [CreateAssetMenu(menuName = "Duels/Unit Spawn Config")]

    public class UnitSpawnConfig : ScriptableObject
    {
        public Unit[] EnemyTeamPrefabs;
        public Unit[] PlayerTeamPrefabs;
    }
}