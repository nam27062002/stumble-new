using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            movementStateMachine.reusableData.movementSpeedModifier = movementData.RunData.SpeedModidier;
        }
        #endregion 
        
        

        #region Input Methods
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            movementStateMachine.ChangeState(movementStateMachine.WalkingState);
        }
        
        #endregion
    }
}