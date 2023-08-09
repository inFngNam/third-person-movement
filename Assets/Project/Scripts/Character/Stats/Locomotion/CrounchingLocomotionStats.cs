using System;
using UnityEngine;

namespace Character.Stats
{
    [Serializable]
    public class CrounchingLocomotionStats
    {
        [field: SerializeField] public float WalkSpeedModifier { get; private set; }
    }
}