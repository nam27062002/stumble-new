using UnityEngine;

namespace MovementSystem
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        public PlayerJumpingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.reusableData.movementSpeedModifier = 0f;
            Jump();
        }
        #endregion


        #region Main Methods
        private void Jump()
        {
            Vector3 jumpForce = stateMachine.reusableData.currentJumpForce;
            Vector3 playerForward = stateMachine.player.transform.forward;
            jumpForce.x *= playerForward.x;
            jumpForce.z *= playerForward.z;
            stateMachine.player.Rigidbody.AddForce(jumpForce,ForceMode.VelocityChange);
        }
        #endregion
    }
}