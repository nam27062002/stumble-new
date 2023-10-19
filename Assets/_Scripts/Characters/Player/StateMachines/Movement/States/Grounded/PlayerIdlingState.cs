using Unity.VisualScripting;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerIdlingState : PlayerMovementState
    {
        public PlayerIdlingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            SpeedModifier = 0f;
            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (MovementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }

        private void OnMove()
        {
            if (shouldWalk)
            {
                MovementStateMachine.ChangeState(MovementStateMachine.WalkingState);
            }
            else
            {
                MovementStateMachine.ChangeState(MovementStateMachine.RunningState);
            }
        }
        #endregion

    }
}