using UnityEngine;

namespace Duels.Units
{
    [CreateAssetMenu(menuName = "Configs/UnitConfig")]
    public class UnitConfig : ScriptableObject
    {
        public string Name;

        public int MaxHealthPoints;

        public int Damage;
    }
}