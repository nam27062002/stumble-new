using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }
        
        
        #region Reusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            movementStateMachine.Player.input.PlayerActions.Move.canceled += OnMovementCanceled;
        }



        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            movementStateMachine.Player.input.PlayerActions.Move.canceled -= OnMovementCanceled;
        }
        
        
        protected virtual void OnMove()
        {
            if (shouldWalk)
            {
                movementStateMachine.ChangeState(movementStateMachine.WalkingState);
            }
            else
            {
                movementStateMachine.ChangeState(movementStateMachine.RunningState);
            }
        }
        #endregion
        
        #region Input Methods
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
        }
        
        #endregion
    }
}