using UnityEngine;

namespace Duels.Units
{
    public class SpawnPoints : MonoBehaviour
    {
        [field: SerializeField] public Transform TeamOne { get; private set; }
        [field: SerializeField] public Transform TeamTwo { get; private set; }
    }
}