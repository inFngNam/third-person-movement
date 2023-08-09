using Character.Controller;
using System;

namespace Character.Locomotion.Grounded.Standing
{
    [Serializable]
    public class SpritingState : GroundedState
    {
        public SpritingState(LocomotionController _controller) : base(_controller)
        {
        }

        public override void Update(LocomotionStatus status)
        {
            base.Update();

            if (!status.IsMoving)
            {
                controller.ChangeState(controller.StandingIdlingState);
                return;
            }

            if (status.IsSprinting)
            {
                return;
            }

            if (status.IsMoving)
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