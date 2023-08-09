using System;
using UnityEngine;

namespace Character.Stats
{
    [Serializable]
    public class StandingLocomotionStats
    {
        [field: SerializeField] public float WalkSpeedModifier { get; private set; }
        [field: SerializeField] public float RunSpeedModifier { get; private set; }
        [field: SerializeField] public float SprintSpeedModifier { get; private set; }
    }
}