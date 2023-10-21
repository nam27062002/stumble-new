using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerRunningState : PlayerMovingState
    {

        private float _startTime;
        private PlayerSprintData _sprintData;
        public PlayerRunningState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
            _sprintData = movementData.sprintData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementSpeedModifier = movementData.runData.SpeedModidier;
            _startTime = Time.time;
            if (!stateMachine.reusableData.shouldWalk) return;
            if (Time.time < _startTime + _sprintData.runToWalkTime) return;
            StopRunning();
        }
        #endregion

        #region MainMethods

        private void StopRunning()
        {
            stateMachine.ChangeState(
                stateMachine.reusableData.movementInput == Vector2.zero ? 
                stateMachine.idlingState :
                stateMachine.walkingState);
        }
        
        #endregion
        
        #region Input Methods
        
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.mediumStoppingState);
        }
        
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            stateMachine.ChangeState(stateMachine.walkingState);
        }
        
        #endregion
    }
}