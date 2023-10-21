namespace MovementSystem.Stopping
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }
        #region IState
        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementDecelerationForce = movementData.stopData.mediumDecelerationForce;
        }
        #endregion
    }
}