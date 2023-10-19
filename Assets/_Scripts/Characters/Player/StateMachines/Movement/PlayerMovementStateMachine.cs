using UnityEngine;

namespace MovementSystem
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player;
        public PlayerIdlingState IdlingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerSprintingState SprintingState { get; }
        
        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            IdlingState = new PlayerIdlingState(this);
            RunningState = new PlayerRunningState(this);
            WalkingState = new PlayerWalkingState(this);
            SprintingState = new PlayerSprintingState(this);
        }
        
        public Vector2 GetMovementInput => Player.GetMovementInput;
        public void SetAddForce(Vector3 movementDir)
        {
            Player.SetAddForce(movementDir);
        }

        public void SetRotation(Quaternion quaternion)
        {
            Player.SetRotation(quaternion);
        }
        public Vector3 PlayerHorizontalVelocity
        {
            get { return Player.PlayerHorizontalVelocity; }
            set { Player.PlayerHorizontalVelocity = value; }
        }

        public Vector3 GetEulerAnglesCamera => Player.GetEulerAnglesCamera;
        public Vector3 GetEulerAnglesPlayer => Player.GetEulerAnglesPlayer;
    }
}