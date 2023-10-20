using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementSpeedModifier = movementData.WalkingData.SpeedModidier;
        }
        #endregion

        #region Input Methods
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            stateMachine.ChangeState(stateMachine.RunningState);
        }
        
        #endregion
        
    }
}