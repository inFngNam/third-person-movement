using System;
using UnityEngine;

namespace Character.Controller
{
    [Serializable]
    public class LocomotionStatus
    {
        [field: SerializeField] public Vector2 DirectionInput { get; private set; }
        [field: SerializeField] public bool IsWalking { get; private set; }
        [field: SerializeField] public bool IsSprinting { get; private set; }
        [field: SerializeField] public bool IsCrouching { get; private set; }
        [field: SerializeField] public bool IsMoving { get; private set; }

        public LocomotionStatus()
        {
            DirectionInput = Vector2.zero;
            IsMoving = false;
            IsWalking = false;
            IsSprinting = false;
            IsCrouching = false;
        }

        public void SetDirectionInput(Vector2 input)
        {
            DirectionInput = input;
            IsMoving = (input == Vector2.zero);
        }

        public void SetSprinting(bool value)
        {
            IsSprinting = value;
        }

        public void SetWalking(bool value)
        {
            IsWalking = value;
        }
    }
}