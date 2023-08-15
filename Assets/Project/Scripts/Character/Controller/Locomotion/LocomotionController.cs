using Character.Locomotion;
using Character.Stats;
using Configure;
using System;
using UnityEngine;

namespace Character.Controllers.Locomotion
{
    public class LocomotionController : MonoBehaviour
    {
        [SerializeField] internal CharacterController characterController;
        [SerializeField] internal LocomotionStatus locomotionStatus;
        [SerializeField] internal Animator m_Animator;

        public Animator GetAnimator() => m_Animator;

        #region StateMachine

        private BaseLocomotionState currentState;

        #region Standing

        [field: Header("States Machine"), Space(5), SerializeField]
        public Character.Locomotion.Grounded.Standing.IdlingState StandingIdlingState { get; private set; }
        [field: SerializeField]
        public Character.Locomotion.Grounded.Standing.WalkingState StandingWalkingState { get; private set; }
        [field: SerializeField]
        public Character.Locomotion.Grounded.Standing.RunningState StandingRunningState { get; private set; }
        [field: SerializeField]
        public Character.Locomotion.Grounded.Standing.SprintingState StandingSprintingState { get; private set; }

        #endregion Standing

        protected void InitializeStateMachine()
        {
            StandingIdlingState = new Character.Locomotion.Grounded.Standing.IdlingState(this);
            StandingWalkingState = new Character.Locomotion.Grounded.Standing.WalkingState(this);
            StandingRunningState = new Character.Locomotion.Grounded.Standing.RunningState(this);
            StandingSprintingState = new Character.Locomotion.Grounded.Standing.SprintingState(this);
        }

        public void ChangeState(BaseLocomotionState newState)
        {
            if (currentState == newState)
            {
                return;
            }

            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        #endregion StateMachine

        #region Stats

        public void OnChangeLocomotionStats(LocomotionStats stats)
        {
            movementSpeed = stats.MovementSpeed;
            jumpHeight = stats.JumpHeight;

            StandingWalkingState.SpeedModifiler = stats.StandingStats.WalkSpeedModifier;
            StandingRunningState.SpeedModifiler = stats.StandingStats.RunSpeedModifier;
            StandingSprintingState.SpeedModifiler = stats.StandingStats.SprintSpeedModifier;
        }

        protected float movementSpeed;
        protected float jumpHeight;

        #endregion

        #region Runtime

        protected bool isGrounded;
        protected Vector3 verticalVelocity;

        #endregion

        #region UnityFunction

        protected virtual void Awake()
        {
            locomotionStatus = new LocomotionStatus();
            InitializeStateMachine();
            ChangeState(StandingIdlingState);
        }

        protected virtual void FixedUpdate()
        {
            ProcessGravity();
        }

        #endregion

        #region Logic

        private void ProcessGravity()
        {
            isGrounded = characterController.isGrounded;

            verticalVelocity.y += EnvironmentConfigure.Gravity * Time.fixedDeltaTime;
            if (isGrounded && verticalVelocity.y < 0)
            {
                verticalVelocity.y = -2;
            }
        }

        public void ProcessMove(Vector2 input)
        {
            Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y).normalized;

            if (inputDirection != Vector3.zero)
            {
                _targetRotation = GetDirectionAngle(inputDirection) + GetCameraDirectionAngle();
                float rotation = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y, _targetRotation,
                    ref _rotationVelocity, RotationSmoothTime
                );
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            Vector3 horizontalVelocity = targetDirection.normalized * movementSpeed;
            characterController.Move((horizontalVelocity + verticalVelocity) * Time.fixedDeltaTime);
        }

        private int GetCameraDirectionAngle()
        {
            throw new NotImplementedException();
        }

        private int GetDirectionAngle(Vector3 inputDirection)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}