using Character.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Controller
{
    public class ProtagonistLocomotionController : LocomotionController
    {
        #region UnityFunction

        protected override void Awake()
        {
            base.Awake();
            GetComponent<Protagonist>().OnChangeLocomotionStats += OnChangeLocomotionStats;
        }

        public void OnDestroy()
        {
            GetComponent<Protagonist>().OnChangeLocomotionStats -= OnChangeLocomotionStats;
        }

        private void FixedUpdate()
        {
            GetDirectionInput();
            ProcessMove();
        }

        #endregion UnityFunction

        private void OnChangeLocomotionStats(LocomotionStats stats)
        {
            movementSpeed = stats.MovementSpeed;
            jumpHeight = stats.JumpHeight;

            StandingWalkingState.SpeedModifiler = stats.StandingStats.WalkSpeedModifier;
            StandingRunningState.SpeedModifiler = stats.StandingStats.RunSpeedModifier;
            StandingSprintingState.SpeedModifiler = stats.StandingStats.SprintSpeedModifier;
        }

        public void OnMove(InputValue value)
        {
            status.DirectionInput = value.Get<Vector2>();
            status.IsMoving = status.DirectionInput != Vector2.zero;
        }

        public void OnSprint(InputValue value)
        {
            status.IsSprinting = value.isPressed;
        }

        protected void ToggleWalk()
        {
            status.IsWalking = !status.IsWalking;
        }

        protected void GetDirectionInput()
        {
            //status.DirectionInput = m_Grouned.Movement.ReadValue<Vector2>();
        }
    }
}