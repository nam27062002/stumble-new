using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerMovementState : IState
    {
        
        protected readonly PlayerMovementStateMachine stateMachine;
        protected readonly PlayerGroundedData movementData;

        protected bool shouldWalk;
        public PlayerMovementState(PlayerMovementStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            movementData = this.stateMachine.Player.data.GroundedData;
            InitializeData();
        }

        private void InitializeData()
        {
            stateMachine.reusableData.TimeToReachTargetRotation = 
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
            stateMachine.reusableData.movementInput = GetMovementInput;
        }

        public virtual void Update()
        {
           
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitionEvent()
        {
        }

        #endregion
        
        #region Main Methods

        private Vector2 GetMovementInput => stateMachine.GetMovementInput;

        private void Move()
        {           
            if (stateMachine.reusableData.movementInput == Vector2.zero || stateMachine.reusableData.movementSpeedModifier == 0f) return;
            Vector3 movementDirection = GetMovementInputDirection;
            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDicrection(targetRotationYAngle);
            float movementSpeed = GetMovementSpeed;
            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            stateMachine.SetAddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity);
            
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
            if (directionAngle != stateMachine.reusableData.CurrentTargetRotation.y)
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
        private Vector3 GetMovementInputDirection => new Vector3(stateMachine.reusableData.movementInput.x,0f,stateMachine.reusableData.movementInput.y);

        private Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.PlayerHorizontalVelocity;
            return new Vector3(playerHorizontalVelocity.x, 0f, playerHorizontalVelocity.z);
        }

        private Vector3 GetEulerAnglesCamera => stateMachine.GetEulerAnglesCamera;
        protected float GetMovementSpeed => movementData.baseSpeed * stateMachine.reusableData.movementSpeedModifier * 
                                          stateMachine.reusableData.movementOnSlopesSpeedModifier;

        protected Vector3 GetPlayerVerticalVelocity => new Vector3(0f,stateMachine.Player.Rigidbody.velocity.y,0f);
        
        private void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.GetEulerAnglesPlayer.y;
            if (currentYAngle == stateMachine.reusableData.CurrentTargetRotation.y)
            {
                return;
            }
            
            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.reusableData.CurrentTargetRotation.y,
                ref stateMachine.reusableData.DampedTargetRotationCurrentCelocity.y, stateMachine.reusableData.TimeToReachTargetRotation.y - stateMachine.reusableData.DampedTargetRotationPassedTime.y);
            stateMachine.reusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0f,smoothedYAngle,0f);
            stateMachine.SetRotation(targetRotation);
            
        }
        
        private void UpdateTargetRotationData(float targetAngle)
        {
            stateMachine.reusableData.CurrentTargetRotation.y = targetAngle;
            stateMachine.reusableData.DampedTargetRotationPassedTime.y = 0f;
        }
        
        private Vector3 GetTargetRotationDicrection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            stateMachine.PlayerHorizontalVelocity = Vector3.zero;
        }
        
        protected virtual void AddInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }
        
        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
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