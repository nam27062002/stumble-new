using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerMovementState : IState
    {
        
        protected readonly PlayerMovementStateMachine movementStateMachine;
        protected readonly PlayerGroundedData movementData;

        protected bool shouldWalk;
        public PlayerMovementState(PlayerMovementStateMachine movementStateMachine)
        {
            this.movementStateMachine = movementStateMachine;
            movementData = this.movementStateMachine.Player.Data.GroundedData;
            InitializeData();
        }

        private void InitializeData()
        {
            movementStateMachine.reusableData.TimeToReachTargetRotation = 
                movementData.BaseRotationData.TargetRotationReachTime;
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
            movementStateMachine.reusableData.movementInput = GetMovementInput;
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

        private Vector2 GetMovementInput => movementStateMachine.GetMovementInput;

        private void Move()
        {           
            if (movementStateMachine.reusableData.movementInput == Vector2.zero || movementStateMachine.reusableData.movementSpeedModifier == 0f) return;
            Vector3 movementDirection = GetMovementInputDirection;
            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDicrection(targetRotationYAngle);
            float movementSpeed = GetMovementSpeed;
            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            movementStateMachine.SetAddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity);
            
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
            if (directionAngle != movementStateMachine.reusableData.CurrentTargetRotation.y)
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
        private Vector3 GetMovementInputDirection => new Vector3(movementStateMachine.reusableData.movementInput.x,0f,movementStateMachine.reusableData.movementInput.y);

        private Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = movementStateMachine.PlayerHorizontalVelocity;
            return new Vector3(playerHorizontalVelocity.x, 0f, playerHorizontalVelocity.z);
        }

        private Vector3 GetEulerAnglesCamera => movementStateMachine.GetEulerAnglesCamera;
        private float GetMovementSpeed => movementData.baseSpeed * movementStateMachine.reusableData.movementSpeedModifier;

        private void RotateTowardsTargetRotation()
        {
            float currentYAngle = movementStateMachine.GetEulerAnglesPlayer.y;
            if (currentYAngle == movementStateMachine.reusableData.CurrentTargetRotation.y)
            {
                return;
            }
            
            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, movementStateMachine.reusableData.CurrentTargetRotation.y,
                ref movementStateMachine.reusableData.DampedTargetRotationCurrentCelocity.y, movementStateMachine.reusableData.TimeToReachTargetRotation.y - movementStateMachine.reusableData.DampedTargetRotationPassedTime.y);
            movementStateMachine.reusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0f,smoothedYAngle,0f);
            movementStateMachine.SetRotation(targetRotation);
            
        }
        
        private void UpdateTargetRotationData(float targetAngle)
        {
            movementStateMachine.reusableData.CurrentTargetRotation.y = targetAngle;
            movementStateMachine.reusableData.DampedTargetRotationPassedTime.y = 0f;
        }
        
        private Vector3 GetTargetRotationDicrection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            movementStateMachine.PlayerHorizontalVelocity = Vector3.zero;
        }
        
        protected virtual void AddInputActionsCallbacks()
        {
            movementStateMachine.Player.input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }



        protected virtual void RemoveInputActionsCallbacks()
        {
            movementStateMachine.Player.input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
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