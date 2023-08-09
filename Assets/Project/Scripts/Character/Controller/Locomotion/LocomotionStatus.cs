using System;
using UnityEngine;

namespace Character.Controller
{
    [Serializable]
    public class LocomotionStatus
    {
        [field: SerializeField] public Vector2 DirectionInput { get; set; }
        [field: SerializeField] public bool IsWalking { get; set; }
        [field: SerializeField] public bool IsSprinting { get; set; }
        [field: SerializeField] public bool IsCrouching { get; set; }
        [field: SerializeField] public bool IsMoving { get; set; }

        public LocomotionStatus()
        {
            DirectionInput = Vector2.zero;
            IsMoving = false;
            IsWalking = false;
            IsSprinting = false;
            IsCrouching = false;
        }
    }
}