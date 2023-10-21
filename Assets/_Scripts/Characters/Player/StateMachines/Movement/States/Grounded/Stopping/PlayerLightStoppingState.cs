namespace MovementSystem.Stopping
{
    public class PlayerLightStoppingState : PlayerStoppingState
    {
        public PlayerLightStoppingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }
        #region IState
        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementDecelerationForce = movementData.stopData.lightDecelerationForce;
        }
        #endregion
    }
}