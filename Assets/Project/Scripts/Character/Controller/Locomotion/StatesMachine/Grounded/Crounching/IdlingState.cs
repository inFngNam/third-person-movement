using Character.Controller;
using System;

namespace Character.Locomotion.Grounded.Crounching
{
    [Serializable]
    public class IdlingState : GroundedState
    {
        public IdlingState(LocomotionController _controller) : base(_controller)
        {
        }
    }
}