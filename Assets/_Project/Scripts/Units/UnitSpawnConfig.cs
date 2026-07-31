using UnityEngine;

namespace Duels.Units
{
    [CreateAssetMenu(menuName = "Duels/Unit Spawn Config")]

    public class UnitSpawnConfig : ScriptableObject
    {
        public GameObject[] TeamOnePrefabs;
        public GameObject[] TeamTwoPrefabs;

        public int CountTeamOnePrefabs()
        {
            int teamOnePrefabNumber = TeamOnePrefabs.Length;
            return teamOnePrefabNumber;
        }

        public int CountTeamTwoPrefabs()
        {
            int teamTwoPrefabNumber = TeamTwoPrefabs.Length;
            return teamTwoPrefabNumber;
        }
    }
}