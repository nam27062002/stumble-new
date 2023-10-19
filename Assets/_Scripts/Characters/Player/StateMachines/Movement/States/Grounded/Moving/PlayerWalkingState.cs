using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerWalkingState : PlayerMovementState
    {
        public PlayerWalkingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            SpeedModifier = 0.225f;
        }
        #endregion

        #region Reusable Methods
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            MovementStateMachine.Player.input.PlayerActions.Move.canceled += OnMovementCanceled;
        }



        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
        }
        #endregion


        #region Input Methods
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            MovementStateMachine.ChangeState(MovementStateMachine.RunningState);
        }
        
        protected void OnMovementCanceled(InputAction.CallbackContext context)
        {
            MovementStateMachine.ChangeState(MovementStateMachine.IdlingState);
        }
        #endregion
        
    }
}