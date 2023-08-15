using System;
using UnityEngine;

namespace Character.Controllers.Locomotion
{
    [Serializable]
    public class LocomotionStatus
    {
        [field: SerializeField] public Vector2 DirectionInput { get; private set; }
        [field: SerializeField] public Vector2 LookInput { get; private set; }
        [field: SerializeField] public bool IsWalking { get; private set; }
        [field: SerializeField] public bool IsSprinting { get; private set; }
        [field: SerializeField] public bool IsMoving { get; private set; }

        public LocomotionStatus()
        {
            DirectionInput = Vector2.zero;
            IsMoving = false;
            IsWalking = false;
            IsSprinting = false;
        }

        public void SetDirectionInput(Vector2 input)
        {
            DirectionInput = input;
            IsMoving = (input != Vector2.zero);
        }

        public void SetLookInput(Vector2 input)
        {
            LookInput = input;
        }

        public void SetSprint(bool value)
        {
            IsSprinting = value;
        }

        public void SetWalk(bool value)
        {
            IsWalking = value;
        }
    }
}