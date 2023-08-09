using System;

namespace infPlugIn.DesignPatterns
{
    public abstract class StateMachine
    {
        [NonSerialized] protected IState currentState;

        public void ChangeState(IState newState)
        {
            if (currentState == newState)
            {
                return;
            }

            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public virtual void HandleInput()
        {
            currentState.HandleInput();
        }

        public virtual void Update()
        {
            currentState.Update();
        }

        public virtual void PhysicsUpdate()
        {
            currentState?.PhysicUpdate();
        }
    }
}