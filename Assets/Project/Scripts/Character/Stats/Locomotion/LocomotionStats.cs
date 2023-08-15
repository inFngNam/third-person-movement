using System;
using UnityEngine;

namespace Character.Stats
{
    [Serializable]
    public class LocomotionStats
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public StandingLocomotionStats StandingStats { get; private set; }
    }
}