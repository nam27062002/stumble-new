namespace MovementSystem
{
    public abstract class StateMachine
    {
        protected IState CurrentState;

        public void ChangeState(IState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        public void HandleInput()
        {
            CurrentState?.HandleInput();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void PhysicsUpdate()
        {
            CurrentState?.PhysicsUpdate();
        }
    }
}