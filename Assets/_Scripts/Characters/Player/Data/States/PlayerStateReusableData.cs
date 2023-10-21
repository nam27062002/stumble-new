using UnityEngine;

namespace MovementSystem
{
    public class PlayerStateReusableData
    {
        public Vector2 movementInput;
        public float movementSpeedModifier = 1f;
        public float movementOnSlopesSpeedModifier = 1f;
        public float movementDecelerationForce = 1f;
        public bool shouldWalk;
        public PlayerRotationData rotationData;
        private Vector3 _currentTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        private Vector3 _dampedTargetRotationCurrentVelocity;
        private Vector3 _dampedTargetRotationPassedTime;
        public Vector3 currentJumpForce;
        public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;
        public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;
        public ref Vector3 DampedTargetRotationCurrentVelocity => ref _dampedTargetRotationCurrentVelocity;
        public ref Vector3 DampedTargetRotationPassedTime => ref _dampedTargetRotationPassedTime;
        
    }
}