using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private readonly PlayerSprintData _sprintData;
        private bool _keepSprinting;
        private float _statetime;
        public PlayerSprintingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
            _sprintData = movementData.sprintData;
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementSpeedModifier = _sprintData.speedModifier;
            _statetime = Time.time;
        }

        public override void Update()
        {
            base.Update();
            if (_keepSprinting) return;
            if (Time.time < _statetime + _sprintData.sprintToRunTime) return;
            StopSprinting();
        }

        public override void Exit()
        {
            base.Exit();
            _keepSprinting = false;
        }

        #endregion

        #region Main Methods
        private void StopSprinting()
        {
            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.hardStoppingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.runningState);
        }
        #endregion
        
        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
        }
        
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.player.Input.PlayerActions.Sprint.performed -= OnSprintPerformed;
        }

        #endregion

        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.hardStoppingState);
        }
        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            _keepSprinting = true;
        }
        #endregion
    }
}