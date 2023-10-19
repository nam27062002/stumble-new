using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine MovementStateMachine;
        protected Vector2 MovementInput;
        protected float BaseSpeed = 5f;
        protected float SpeedModifier = 1f;
        protected Vector3 currentTargetRotation;
        protected Vector3 timeToReachTargetRotation;
        protected Vector3 dampedTargetRotationCurrentVelocity;
        protected Vector3 dampedTargetRotationPassedTime;

        protected bool shouldWalk;
        public PlayerMovementState(PlayerMovementStateMachine movementStateMachine)
        {
            MovementStateMachine = movementStateMachine;
            InitializeData();
        }

        private void InitializeData()
        {
            timeToReachTargetRotation.y = 0.14f;
            
        }
        
        #region IState Methods
        public virtual void Enter()
        {
            Debug.Log($"State: {GetType().Name}");
            AddInputActionsCallbacks();
        }



        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }



        public virtual void HandleInput()
        {
            MovementInput = GetMovementInput;
        }

        public virtual void Update()
        {
           
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }
        
        #endregion
        
        #region Main Methods

        private Vector2 GetMovementInput => MovementStateMachine.GetMovementInput;

        private void Move()
        {
            if (MovementInput == Vector2.zero || SpeedModifier == 0f) return;
            Vector3 movementDirection = GetMovementInputDirection;
            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDicrection(targetRotationYAngle);
            float movementSpeed = GetMovementSpeed;
            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            MovementStateMachine.SetAddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity);
            
        }
        
        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);
            RotateTowardsTargetRotation();
            return directionAngle;
        }


        #endregion

        
        #region Reusable Methods

        private float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);
            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }
            if (directionAngle != currentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }
        
        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        private float AddCameraRotationToAngle(float angle)
        {
            angle += GetEulerAnglesCamera.y;

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }
        private Vector3 GetMovementInputDirection => new Vector3(MovementInput.x,0f,MovementInput.y);

        private Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = MovementStateMachine.PlayerHorizontalVelocity;
            return new Vector3(playerHorizontalVelocity.x, 0f, playerHorizontalVelocity.z);
        }

        private Vector3 GetEulerAnglesCamera => MovementStateMachine.GetEulerAnglesCamera;
        private float GetMovementSpeed => BaseSpeed * SpeedModifier;

        private void RotateTowardsTargetRotation()
        {
            float currentYAngle = MovementStateMachine.GetEulerAnglesPlayer.y;
            if (currentYAngle == currentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation.y,
                ref dampedTargetRotationCurrentVelocity.y, timeToReachTargetRotation.y - dampedTargetRotationPassedTime.y);
            dampedTargetRotationPassedTime.y += Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0f,smoothedYAngle,0f);
            MovementStateMachine.SetRotation(targetRotation);
            
        }
        
        private void UpdateTargetRotationData(float targetAngle)
        {
            currentTargetRotation.y = targetAngle;
            dampedTargetRotationPassedTime.y = 0f;
        }
        
        private Vector3 GetTargetRotationDicrection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            MovementStateMachine.PlayerHorizontalVelocity = Vector3.zero;
        }
        
        protected virtual void AddInputActionsCallbacks()
        {
            MovementStateMachine.Player.input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }



        protected virtual void RemoveInputActionsCallbacks()
        {
            MovementStateMachine.Player.input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        }
        #endregion

        #region Input Methods
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            shouldWalk = !shouldWalk;
        }
        #endregion
    }
}