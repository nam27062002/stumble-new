using MovementSystem.Stopping;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public readonly Player player;
        public readonly PlayerStateReusableData reusableData;

       
        // MOVE ON GROUND
        public readonly PlayerRunningState runningState;
        public readonly PlayerWalkingState walkingState;
        public readonly PlayerDashingState dashingState;
        public readonly PlayerSprintingState sprintingState;
        
        // IDLE
        public readonly PlayerIdlingState idlingState;
        public readonly PlayerHardStoppingState hardStoppingState;
        public readonly PlayerMediumStoppingState mediumStoppingState;
        public readonly PlayerLightStoppingState lightStoppingState;
        
        // AIRBORNE
        public readonly PlayerJumpingState jumpingState;
        public PlayerMovementStateMachine(Player player)
        {
            this.player = player;
            reusableData = new PlayerStateReusableData();
            idlingState = new PlayerIdlingState(this);
            runningState = new PlayerRunningState(this);
            walkingState = new PlayerWalkingState(this);
            sprintingState = new PlayerSprintingState(this);
            dashingState = new PlayerDashingState(this);
            hardStoppingState = new PlayerHardStoppingState(this);
            mediumStoppingState = new PlayerMediumStoppingState(this);
            lightStoppingState = new PlayerLightStoppingState(this);
            jumpingState = new PlayerJumpingState(this);

        }
        
        public Vector2 GetMovementInput => player.GetMovementInput;
        public void SetAddForce(Vector3 movementDir)
        {
            player.SetAddForce(movementDir);
        }

        public void SetRotation(Quaternion quaternion)
        {
            player.SetRotation(quaternion);
        }
        public Vector3 PlayerHorizontalVelocity
        {
            get { return player.PlayerHorizontalVelocity; }
            set { player.PlayerHorizontalVelocity = value; }
        }

        public Vector3 GetEulerAnglesCamera => player.GetEulerAnglesCamera;
        public Vector3 GetEulerAnglesPlayer => player.GetEulerAnglesPlayer;
    }
}