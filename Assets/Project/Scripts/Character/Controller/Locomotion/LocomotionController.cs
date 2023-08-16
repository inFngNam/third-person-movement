using Character.Locomotion;
using Character.Stats;
using Configure;
using UnityEngine;

namespace Character.Controllers.Locomotion
{
    public class LocomotionController : MonoBehaviour
    {
        internal CharacterController m_CharacterController;
        internal Animator m_Animator;

        #region Animator

        private int MovementSpeedHash;
        private int MotionSpeedHash;

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
            currentState.Update(m_LocomotionStatus);
        }

        #endregion StateMachine

        #region Stats

        protected float baseSpeed;
        protected float jumpHeight;

        #endregion

        #region Consts

        protected const float RotationSmoothTime = 0.12f;
        protected const float SpeedChangeRate = 10.0f;

        #endregion Consts

        #region Runtime

        [Header("Runtime")]
        [SerializeField] internal LocomotionStatus m_LocomotionStatus;
        protected bool m_IsGrounded;
        protected Vector3 m_VerticalVelocity;
        protected float m_TargetRotation;
        protected float m_RotationVelocity;
        protected float m_MovementSpeed;
        protected float m_AnimationBlend;

        #endregion Runtime

        #region UnityFunction

        protected virtual void Awake()
        {
            m_LocomotionStatus = new LocomotionStatus();
            InitializeStateMachine();
            InitializeAnimator();
            ChangeState(StandingIdlingState);
        }

        protected virtual void Update()
        {
            ProcessGravity();
            ProcessMove();
        }

        #endregion

        #region Initialize

        public void OnChangeLocomotionStats(LocomotionStats stats)
        {
            baseSpeed = stats.MovementSpeed;
            jumpHeight = stats.JumpHeight;

            StandingWalkingState.SpeedModifiler = stats.StandingStats.WalkSpeedModifier;
            StandingRunningState.SpeedModifiler = stats.StandingStats.RunSpeedModifier;
            StandingSprintingState.SpeedModifiler = stats.StandingStats.SprintSpeedModifier;
        }

        protected virtual void InitializeAnimator()
        {

        }

        #endregion StatsChange

        #region Logic

        protected virtual void ProcessGravity()
        {
            m_IsGrounded = m_CharacterController.isGrounded;
            m_VerticalVelocity.y += EnvironmentConfigure.Gravity * Time.deltaTime;
            if (m_IsGrounded && m_VerticalVelocity.y < 0)
            {
                m_VerticalVelocity.y = -2;
            }
        }

        protected virtual void ProcessMove()
        {
            Vector2 input = m_LocomotionStatus.DirectionInput;
            float inputMagnitude = input.magnitude;

            float targetSpeed = GetTargetSpeed();
            float currentSpeed = GetCurrentSpeed();

            float speedOffset = 0.1f;

            if (currentSpeed < targetSpeed - speedOffset || currentSpeed > targetSpeed + speedOffset)
            {
                m_MovementSpeed = Mathf.Lerp(currentSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
                m_MovementSpeed = Mathf.Round(m_MovementSpeed * 1000f) / 1000f;
            }
            else
            {
                m_MovementSpeed = targetSpeed;
            }

            Vector3 moveDirection = new Vector3(input.x, 0.0f, input.y).normalized;

            m_AnimationBlend = Mathf.Lerp(m_AnimationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

            if (input != Vector2.zero)
            {
                m_TargetRotation = GetDirectionAngle(moveDirection);
                float rotation = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y, m_TargetRotation, ref m_RotationVelocity, RotationSmoothTime
                );
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, m_TargetRotation, 0.0f) * Vector3.forward;
            Vector3 horizontalVelocity = targetDirection * m_MovementSpeed;
            m_CharacterController.Move((horizontalVelocity + m_VerticalVelocity) * Time.deltaTime);
        }

        protected virtual void ProcessAnimatior()
        {
            m_AnimationBlend = m_AnimationBlend < 0.01 ? 0 : m_AnimationBlend;
            //m_Animator.SetFloat(_animIDSpeed, _animationBlend);
            //m_Animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }

        protected float GetDirectionAngle(Vector3 moveDirection)
        {
            return Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        }

        protected float GetTargetSpeed()
        {
            return currentState.SpeedModifiler * baseSpeed;
        }

        protected float GetCurrentSpeed()
        {
            return new Vector2(m_CharacterController.velocity.x, m_CharacterController.velocity.z).magnitude;
        }

        #endregion
    }
}