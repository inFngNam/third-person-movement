using Character.Controller;
using System;

namespace Character.Locomotion.Grounded.Standing
{
    [Serializable]
    public class WalkingState : GroundedState
    {
        public WalkingState(LocomotionController _controller) : base(_controller)
        {
        }

        public override void Enter()
        {
            base.Enter();
            controller.GetAnimator().SetFloat("movement_speed", 0.5f);
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

            if (!status.IsWalking)
            {
                controller.ChangeState(controller.StandingRunningState);
            }
        }
    }
}