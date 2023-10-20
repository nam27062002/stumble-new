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
        
        public void OnAnimationEnterEvent()
        {
            CurrentState?.OnAnimationEnterEvent();
        }

        public void OnAnimationExitEvent()
        {
            CurrentState?.OnAnimationExitEvent();
        }

        public void OnAnimationTransitionEvent()
        {
            CurrentState?.OnAnimationTransitionEvent();
        }
    }
}