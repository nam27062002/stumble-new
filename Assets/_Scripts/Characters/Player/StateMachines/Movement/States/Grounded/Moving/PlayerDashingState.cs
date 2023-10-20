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
        public PlayerDashingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            _dashData = movementData.DashData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementSpeedModifier = _dashData.speedModifier;
            AddForceOnTransitionFromStationaryState();
            UpdateConsecutiveDashes();
            _startTime = Time.time;
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();
            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.SprintingState);
        }

        #endregion



        #region Main Methods
        private void AddForceOnTransitionFromStationaryState()
        {
            if (stateMachine.reusableData.movementInput != Vector2.zero)
            {
                return;
            }

            Vector3 characterDirection = stateMachine.Player.transform.forward;
            characterDirection.y = 0f;
            stateMachine.Player.Rigidbody.velocity = characterDirection * GetMovementSpeed;
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
                stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash,_dashData.dashLimitReachedCooldown);
            }
        }

        private bool IsConsecutive()
        {
            return Time.time < _startTime + _dashData.timeTobeConsideredConsecutive;
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
            
        }
        #endregion

    }

}
