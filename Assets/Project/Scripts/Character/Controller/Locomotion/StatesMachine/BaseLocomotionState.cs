using Character.Controller;
using UnityEngine;
using infPlugIn.DesignPatterns;

namespace Character.Locomotion
{
    public class BaseLocomotionState : IState
    {
        [HideInInspector] protected LocomotionController controller;
        [SerializeField] protected bool isCurrentState;
        [field: SerializeField] public float SpeedModifiler { get; set; }

        public BaseLocomotionState(LocomotionController _controller)
        {
            controller = _controller;
        }

        public virtual void Enter()
        {
            Debug.Log($"State: {GetType()}");
            isCurrentState = true;
        }

        public virtual void Exit()
        {
            isCurrentState = false;
        }

        public virtual void HandleInput()
        {
        }

        public virtual void PhysicUpdate()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Update(LocomotionStatus status)
        {
        }

        protected virtual void OnMove(LocomotionStatus status)
        {
        }
    }
}