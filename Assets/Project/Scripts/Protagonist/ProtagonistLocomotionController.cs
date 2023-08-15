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

        protected override void FixedUpdate()
        {
            ProcessGravity();
            ProcessMove();
        }

        #endregion UnityFunction

        public void SetControllerConfigures(ProtagonistControllerConfigure _controllerConfigures)
        {
            configure = _controllerConfigures;
        }

        public void OnMove(InputValue value)
        {
            locomotionStatus.SetDirectionInput(value.Get<Vector2>());
            UpdateState();
        }

        protected void OnLook(InputValue value)
        {
            if (configure.MoveRelativeToCamera)
            {
                locomotionStatus.SetLookInput(value.Get<Vector2>());
            }
        }

        public void OnSprint(InputValue value)
        {
            locomotionStatus.SetSprint(value.isPressed);
        }

        protected void OnToggleWalk(InputValue value)
        {
            if (value.isPressed)
            {
                locomotionStatus.SetWalk(!locomotionStatus.IsWalking);
            }
        }
    }
}