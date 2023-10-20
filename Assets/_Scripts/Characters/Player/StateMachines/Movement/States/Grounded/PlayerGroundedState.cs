using _Scripts.Data.Colliders;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerGroundedState : PlayerMovementState
    {
        private readonly SlopeData _slopeData;
        protected PlayerGroundedState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            _slopeData = stateMachine.Player.colliderUtility.slopeData;
        }

        #region IState Methods

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Float();
        }
        
        #endregion

        #region Main Methods

        private void Float()
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.colliderUtility
                .CapsuleColliderData.Collider.bounds.center;
            
            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);
            
            if (Physics.Raycast
                    (downwardsRayFromCapsuleCenter, 
                    out RaycastHit hit, 
                    _slopeData.floatRayDistance,
                    stateMachine.Player.layerData.groundLayer, 
                    QueryTriggerInteraction.Ignore)
                )
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);
                float slopeAngleWithPlayerForward = Vector3.Angle(hit.normal, stateMachine.Player.transform.forward);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle,slopeAngleWithPlayerForward);

                if (slopeSpeedModifier == 0f)
                {
                    return;
                }
                float distanceToFloatingPoint 
                    = stateMachine.Player.colliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y 
                      * stateMachine.Player.transform.localScale.y - hit.distance;
                if (distanceToFloatingPoint == 0) return;
                float amountToFift = distanceToFloatingPoint * _slopeData.stepReachForce - GetPlayerVerticalVelocity.y;
                Vector3 liftForce = new Vector3(0f, amountToFift, 0f);
                stateMachine.Player.Rigidbody.AddForce(liftForce,ForceMode.VelocityChange);
            }
        }

        
        #endregion
        
        #region Reusable Methods
        
        private float SetSlopeSpeedModifierOnAngle(float angle,float slopeAngleWithPlayerForward)
        {
            float slopeSpeedModifier = stateMachine.Player.data.GroundedData.SlopeSpeedAngles.Evaluate(angle);
            
            slopeSpeedModifier = IsPlayerClimbingSlope(slopeAngleWithPlayerForward)
                ? slopeSpeedModifier
                : 2 - slopeSpeedModifier;
            stateMachine.reusableData.movementOnSlopesSpeedModifier = slopeSpeedModifier;
            return slopeSpeedModifier;
        }

        private bool IsPlayerClimbingSlope(float slopeAngleWithPlayerForward)
        {
            return slopeAngleWithPlayerForward > 90f;
        }
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Move.canceled += OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;
        }
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.Player.Input.PlayerActions.Move.canceled -= OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStarted;
        }
        
        
        protected virtual void OnMove()
        {
            stateMachine.ChangeState(shouldWalk ? stateMachine.WalkingState : stateMachine.RunningState);
        }
        #endregion
        
        #region Input Methods
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.DashingState);
        }
        #endregion
    }
}