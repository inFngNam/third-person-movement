using Character.Controllers.Locomotion;
using System;

namespace Character.Locomotion.Grounded.Standing
{
    [Serializable]
    public class RunningState : GroundedState
    {
        public RunningState(LocomotionController _controller) : base(_controller)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update(LocomotionStatus status)
        {
            base.Update(status);

            if (!status.IsMoving)
            {
                controller.ChangeState(controller.StandingIdlingState);
                return;
            }

            if (status.IsSprinting)
            {
                controller.ChangeState(controller.StandingSprintingState);
                return;
            }

            if (status.IsWalking)
            {
                controller.ChangeState(controller.StandingWalkingState);
            }
        }
    }
}