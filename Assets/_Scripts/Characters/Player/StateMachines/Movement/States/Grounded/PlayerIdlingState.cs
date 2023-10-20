﻿using Unity.VisualScripting;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementSpeedModifier = 0f;
            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }


        #endregion

    }
}