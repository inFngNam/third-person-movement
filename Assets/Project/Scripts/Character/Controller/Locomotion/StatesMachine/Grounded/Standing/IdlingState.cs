using Character.Controllers.Locomotion;
using System;

namespace Character.Locomotion.Grounded.Standing
{
    [Serializable]
    public class IdlingState : GroundedState
    {
        public IdlingState(LocomotionController _controller) : base(_controller)
        {
        }

        public override void Update(LocomotionStatus status)
        {
            base.Update(status);

            if (!status.IsMoving)
            {
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
            else
            {
                controller.ChangeState(controller.StandingRunningState);
            }
        }
    }
}