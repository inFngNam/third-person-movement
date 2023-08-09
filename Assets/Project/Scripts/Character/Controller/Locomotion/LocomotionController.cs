using Character.Locomotion;
using UnityEngine;

namespace Character.Controller
{
    [RequireComponent(typeof(Locomotion))]
    public class LocomotionController : MonoBehaviour
    {
        protected Locomotion locomotion;
        [SerializeField] protected LocomotionStatus status;
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
        public Character.Locomotion.Grounded.Standing.SpritingState StandingSpritingState { get; private set; }

        #endregion Standing

        #region Crounching

        [field: SerializeField]
        public Character.Locomotion.Grounded.Crounching.IdlingState CrounchingIdlingState { get; private set; }

        #endregion Crounching

        protected void InitializeStateMachine()
        {
            StandingIdlingState = new Character.Locomotion.Grounded.Standing.IdlingState(this);
            StandingWalkingState = new Character.Locomotion.Grounded.Standing.WalkingState(this);
            StandingRunningState = new Character.Locomotion.Grounded.Standing.RunningState(this);
            StandingSpritingState = new Character.Locomotion.Grounded.Standing.SpritingState(this);

            CrounchingIdlingState = new Character.Locomotion.Grounded.Crounching.IdlingState(this);
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

        protected virtual void Awake()
        {
            locomotion = GetComponent<Locomotion>();
            status = new LocomotionStatus();
            InitializeStateMachine();
            ChangeState(StandingIdlingState);
        }

        protected virtual void ProcessMove()
        {
            currentState.Update(status);
            locomotion.ProcessMove(status.DirectionInput, movementSpeed * currentState.SpeedModifiler);
        }

        protected virtual void Jump()
        {
            locomotion.ProcessJump(jumpHeight);
        }
    }
}