using Character.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Controller
{
    public class ProtagonistLocomotionController : LocomotionController
    {
        #region UnityFunction

        private void OnEnable()
        {
            // m_Grouned.Enable();
        }

        private void OnDisable()
        {
            // m_Grouned.Disable();
        }

        protected override void Awake()
        {
            base.Awake();
            InitializeInput();
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
            StandingSpritingState.SpeedModifiler = stats.StandingStats.SprintSpeedModifier;
        }

        public void OnMove(InputValue value)
        {
            Debug.Log(value);
            // MoveInput(value.Get<Vector2>());
        }

        private void InitializeInput()
        {
            //    UserInput m_Input = new UserInput();
            //    m_Grouned = m_Input.Grounded;

            //    m_Grouned.ToggleWalk.performed += (_) => ToggleWalk();

            //    m_Grouned.Sprint.performed += (_) => SetSprint(true);
            //    m_Grouned.Sprint.canceled += (_) => SetSprint(false);

            //    m_Grouned.Jump.performed += (_) => Jump();
        }

        protected void SetSprint(bool value)
        {
            status.IsSprinting = value;
        }

        protected void ToggleWalk()
        {
            status.IsWalking = !status.IsWalking;
        }

        protected void GetDirectionInput()
        {
            //status.DirectionInput = m_Grouned.Movement.ReadValue<Vector2>();
            status.IsMoving = status.DirectionInput != Vector2.zero;
        }
    }
}