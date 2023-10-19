using UnityEngine;

namespace MovementSystem
{
    public class PlayerStateReusableData
    {
        public Vector2 movementInput;
        public float movementSpeedModifier;
        public bool shouldWalk;
        private Vector3 _currentTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        private Vector3 _dampedTargetRotationCurrentCelocity;
        private Vector3 _dampedTargetRotationPassedTime;
        public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;
        public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;
        public ref Vector3 DampedTargetRotationCurrentCelocity => ref _dampedTargetRotationCurrentCelocity;
        public ref Vector3 DampedTargetRotationPassedTime => ref _dampedTargetRotationPassedTime;
    }
}