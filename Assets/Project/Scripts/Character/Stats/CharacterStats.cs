using Character.Stats;
using System;
using UnityEngine;

namespace Character.Stats
{
    [Serializable]
    public class CharacterStats
    {
        [field: SerializeField] public LocomotionStats LocomotionStats { get; private set; }
    }
}