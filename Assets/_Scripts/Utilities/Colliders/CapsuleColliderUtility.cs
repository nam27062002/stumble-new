using System;
using _Scripts.Data.Colliders;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Utilities.Colliders
{
    [Serializable]
    public class CapsuleColliderUtility
    {
        public CapsuleColliderData CapsuleColliderData;
        public DefaultColliderData defaultColliderData;
        public SlopeData slopeData;
    
        public void Initialize(CapsuleCollider collider)
        {
            if (CapsuleColliderData != null) return;
            CapsuleColliderData = new CapsuleColliderData();
            CapsuleColliderData.Initialize(collider);
        }
        public void CalculateCapsuleColliderDimensions()
        {
            SetCapsuleColliderRadius(defaultColliderData.radius);
            SetCapsuleColliderHeight(defaultColliderData.height * (1f - slopeData.stepHeightPercentage));
            RecalculateCapsuleColliderCenter();
            float halfColliderHeight = CapsuleColliderData.Collider.height / 2f;
            if (halfColliderHeight < CapsuleColliderData.Collider.radius)
            {
                SetCapsuleColliderRadius(halfColliderHeight / 2f);
            }

            CapsuleColliderData.UpdateColliderData();
        }

        private void RecalculateCapsuleColliderCenter()
        {
            float colliderHeightDiffrence = defaultColliderData.height - CapsuleColliderData.Collider.height;
            Vector3 newColliderCenter = new Vector3(0f, defaultColliderData.centerY + (colliderHeightDiffrence / 2f), 0f);
            CapsuleColliderData.Collider.center = newColliderCenter;
        }

        private void SetCapsuleColliderRadius(float radius)
        {
            CapsuleColliderData.Collider.radius = radius;
        }
    
        private void SetCapsuleColliderHeight(float height)
        {
            CapsuleColliderData.Collider.height = height;
        }
    
    }
}
