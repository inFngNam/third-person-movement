using Character.Controllers.Locomotion;
using Protagonist.Configures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Protagonist
{
    public class ProtagonistLocomotionController : LocomotionController
    {
        [Header("Configure")]
        [SerializeField] private ProtagonistControllerConfigure configure;

        #region UnityFunction

        protected override void Awake()
        {
            base.Awake();
            m_CharacterController = GetComponent<CharacterController>();
            m_Animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            ProcessGravity();
            ProcessMove();
            ProcessAnimatior();
        }

        #endregion UnityFunction

        #region Initialize

        public void SetControllerConfigures(ProtagonistControllerConfigure _controllerConfigures)
        {
            configure = _controllerConfigures;
        }

        #endregion Initialize

        #region InputSystemCallbacks

        public void OnMove(InputValue value)
        {
            m_LocomotionStatus.SetDirectionInput(value.Get<Vector2>());
            UpdateState();
        }

        protected void OnLook(InputValue value)
        {
            if (configure.MoveRelativeToCamera)
            {
                m_LocomotionStatus.SetLookInput(value.Get<Vector2>());
            }
        }

        public void OnSprint(InputValue value)
        {
            m_LocomotionStatus.SetSprint(value.isPressed);
        }

        protected void OnToggleWalk(InputValue value)
        {
            if (value.isPressed)
            {
                m_LocomotionStatus.SetWalk(!m_LocomotionStatus.IsWalking);
                UpdateState();
            }
        }

        #endregion InputSystemCallbacks

        #region Logic

        #endregion Logic
    }
}