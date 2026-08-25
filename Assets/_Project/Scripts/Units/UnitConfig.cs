using UnityEngine;

namespace Duels.Units
{
    [CreateAssetMenu(menuName = "Configs/UnitConfig")]

    public class UnitConfig : ScriptableObject
    {
        public UnitType UnitType;
        
        public string Name;

        public int MaxHealthPoints;

        public int Damage;
    }
}