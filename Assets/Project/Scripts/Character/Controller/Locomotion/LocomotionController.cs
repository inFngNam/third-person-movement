using Character.Locomotion;
using Character.Stats;
using UnityEngine;

namespace Character.Controller
{
    [RequireComponent(typeof(Locomotion))]
    public class LocomotionController : MonoBehaviour
    {
        protected Locomotion locomotion;
        [SerializeField] protected LocomotionStatus locomotionStatus;
        [SerializeField] protected Animator m_Animator;

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

        protected float movementSpeed;
        protected float jumpHeight;

        #endregion

        public void OnChangeLocomotionStats(LocomotionStats stats)
        {
            movementSpeed = stats.MovementSpeed;
            jumpHeight = stats.JumpHeight;

            StandingWalkingState.SpeedModifiler = stats.StandingStats.WalkSpeedModifier;
            StandingRunningState.SpeedModifiler = stats.StandingStats.RunSpeedModifier;
            StandingSprintingState.SpeedModifiler = stats.StandingStats.SprintSpeedModifier;
        }

        protected virtual void Awake()
        {
            locomotion = GetComponent<Locomotion>();
            locomotionStatus = new LocomotionStatus();
            InitializeStateMachine();
            ChangeState(StandingIdlingState);
        }

        protected virtual void ProcessMove()
        {
            currentState.Update(locomotionStatus);
            locomotion.ProcessMove(locomotionStatus.DirectionInput, movementSpeed * currentState.SpeedModifiler);
        }

        protected virtual void Jump()
        {
            locomotion.ProcessJump(jumpHeight);
        }
    }
}