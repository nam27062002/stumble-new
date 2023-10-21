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
            _slopeData = stateMachine.player.colliderUtility.slopeData;
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
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.player.colliderUtility
                .CapsuleColliderData.Collider.bounds.center;
            
            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);
            
            if (Physics.Raycast
                    (downwardsRayFromCapsuleCenter, 
                    out RaycastHit hit, 
                    _slopeData.floatRayDistance,
                    stateMachine.player.layerData.groundLayer, 
                    QueryTriggerInteraction.Ignore)
                )
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);
                float slopeAngleWithPlayerForward = Vector3.Angle(hit.normal, stateMachine.player.transform.forward);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle,slopeAngleWithPlayerForward);

                if (slopeSpeedModifier == 0f)
                {
                    return;
                }
                float distanceToFloatingPoint 
                    = stateMachine.player.colliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y 
                      * stateMachine.player.transform.localScale.y - hit.distance;
                if (distanceToFloatingPoint == 0) return;
                float amountToFift = distanceToFloatingPoint * _slopeData.stepReachForce - GetPlayerVerticalVelocity.y;
                Vector3 liftForce = new Vector3(0f, amountToFift, 0f);
                stateMachine.player.Rigidbody.AddForce(liftForce,ForceMode.VelocityChange);
            }
        }

        
        #endregion
        
        #region Reusable Methods
        
        private float SetSlopeSpeedModifierOnAngle(float angle,float slopeAngleWithPlayerForward)
        {
            float slopeSpeedModifier = stateMachine.player.data.GroundedData.slopeSpeedAngles.Evaluate(angle);
            
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
            stateMachine.player.Input.PlayerActions.Move.canceled += OnMovementCanceled;
            stateMachine.player.Input.PlayerActions.Dash.started += OnDashStarted;
        }
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            stateMachine.player.Input.PlayerActions.Move.canceled -= OnMovementCanceled;
            stateMachine.player.Input.PlayerActions.Dash.started -= OnDashStarted;
        }
        
        
        protected virtual void OnMove()
        {
            stateMachine.ChangeState(stateMachine.reusableData.shouldWalk ? stateMachine.walkingState : stateMachine.runningState);
        }
        #endregion
        
        #region Input Methods
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.idlingState);
        }
        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.dashingState);
        }
        #endregion
    }
}