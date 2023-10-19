using Unity.VisualScripting;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            movementStateMachine.reusableData.movementSpeedModifier = 0f;
            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (movementStateMachine.reusableData.movementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }


        #endregion

    }
}