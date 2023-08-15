using Character.Locomotion;
using Character.Stats;
using Configure;
using System;
using UnityEngine;

namespace Character.Controllers.Locomotion
{
    public class LocomotionController : MonoBehaviour
    {
        internal CharacterController m_CharacterController;

        #region Animator

        internal Animator m_Animator;

        public Animator GetAnimator() => m_Animator;

        protected virtual void InitializeAnimator()
        {

        }

        #endregion Animator

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

        protected virtual void UpdateState()
        {
            currentState.Update(locomotionStatus);
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

        [Header("Runtime")]
        [SerializeField] internal LocomotionStatus locomotionStatus;
        protected bool isGrounded;
        protected Vector3 m_VerticalVelocity;
        protected float m_TargetRotation;
        protected float m_RotationVelocity;
        protected const float RotationSmoothTime = 0.12f;

        #endregion

        #region UnityFunction

        protected virtual void Awake()
        {
            locomotionStatus = new LocomotionStatus();
            InitializeStateMachine();
            InitializeAnimator();
            ChangeState(StandingIdlingState);
        }

        protected virtual void FixedUpdate()
        {
            ProcessGravity();
            ProcessMove();
        }

        #endregion

        #region Logic

        protected virtual void ProcessGravity()
        {
            isGrounded = m_CharacterController.isGrounded;

            m_VerticalVelocity.y += EnvironmentConfigure.Gravity * Time.fixedDeltaTime;
            if (isGrounded && m_VerticalVelocity.y < 0)
            {
                m_VerticalVelocity.y = -2;
            }
        }

        protected virtual void ProcessMove()
        {
            Vector2 input = locomotionStatus.DirectionInput;
            Vector3 moveDirection = new Vector3(input.x, 0.0f, input.y).normalized;

            if (moveDirection != Vector3.zero)
            {
                m_TargetRotation = GetDirectionAngle(moveDirection);
                float rotation = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y, m_TargetRotation,
                    ref m_RotationVelocity, RotationSmoothTime
                );
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, m_TargetRotation, 0.0f) * Vector3.forward;
            Vector3 horizontalVelocity = targetDirection.normalized * GetMovementSpeed();
            m_CharacterController.Move((horizontalVelocity + m_VerticalVelocity) * Time.fixedDeltaTime);
        }

        protected float GetDirectionAngle(Vector3 moveDirection)
        {
            return Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        }

        protected float GetMovementSpeed()
        {
            return currentState.SpeedModifiler * movementSpeed;
        }

        #endregion
    }

}