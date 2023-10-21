using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerStoppingState : PlayerGroundedState
    {
        protected PlayerStoppingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            RotateTowardsTargetRotation();
            stateMachine.reusableData.movementSpeedModifier = 0f;
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            if (!IsMovingHorizontally()) return;
            DecelerateHorizontally();
        }

        public override void OnAnimationTransitionEvent()
        {
            stateMachine.ChangeState(stateMachine.idlingState);
        }

        #endregion

        #region Reuseable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.player.Input.PlayerActions.Move.started += OnMovementStart;
        }


        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.player.Input.PlayerActions.Move.started -= OnMovementStart;
        }

        #endregion
        
        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {

        }

        private void OnMovementStart(InputAction.CallbackContext context)
        {
           OnMove();
        }
        #endregion
    }
}