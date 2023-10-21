using MovementSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData _dashData;
        private float _startTime;
        private int _consecutiveDashesUsed;
        private bool _shouldKeepRotating;
        public PlayerDashingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            _dashData = movementData.dashData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementSpeedModifier = _dashData.speedModifier;
            stateMachine.reusableData.rotationData = _dashData.rotationData;
            AddForceOnTransitionFromStationaryState();
            _shouldKeepRotating = stateMachine.reusableData.movementInput != Vector2.zero;
            UpdateConsecutiveDashes();
            _startTime = Time.time;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if (!_shouldKeepRotating) return;
            RotateTowardsTargetRotation();
        }

        public override void Exit()
        {
            base.Exit();
            SetbaseRotationData();
        }

        public override void OnAnimationTransitionEvent()
        {
            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.idlingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.sprintingState);
        }

        #endregion



        #region Main Methods
        private void AddForceOnTransitionFromStationaryState()
        {
            if (stateMachine.reusableData.movementInput != Vector2.zero)
            {
                return;
            }

            Vector3 characterDirection = stateMachine.player.transform.forward;
            characterDirection.y = 0f;
            UpdateTargetRotation(characterDirection,false);
            stateMachine.player.Rigidbody.velocity = characterDirection * GetMovementSpeed;
        }

        private void UpdateConsecutiveDashes()
        {
            if (IsConsecutive())
            {
                _consecutiveDashesUsed = 0;
            }

            ++_consecutiveDashesUsed;
            if (_consecutiveDashesUsed == _dashData.consecutiveDashesLimitAmount)
            {
                _consecutiveDashesUsed = 0;
                stateMachine.player.Input.DisableActionFor(stateMachine.player.Input.PlayerActions.Dash,_dashData.dashLimitReachedCooldown);
            }
        }

        private bool IsConsecutive()
        {
            return Time.time < _startTime + _dashData.timeTobeConsideredConsecutive;
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.player.Input.PlayerActions.Move.performed += OnMovementPerformed;
        }
        
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.player.Input.PlayerActions.Move.performed -= OnMovementPerformed;
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            _shouldKeepRotating = true;
        }
        #endregion

    }

}
