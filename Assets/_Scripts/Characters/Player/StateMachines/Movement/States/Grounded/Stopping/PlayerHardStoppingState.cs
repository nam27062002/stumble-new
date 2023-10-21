namespace MovementSystem.Stopping
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState

        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementDecelerationForce = movementData.stopData.hardDecelerationForce;
        }

        #endregion

        #region Reusable Methods

        protected override void OnMove()
        {
            if (stateMachine.reusableData.shouldWalk)
            {
                return;
            }
            stateMachine.ChangeState(stateMachine.runningState);
        }

        #endregion

    }
}