using System;
using _Scripts.Data.Colliders;
using UnityEngine;

[Serializable]
public class CapsuleColliderUtility
{
    public CapsuleColliderData capsuleColliderData;
    public DefaultColliderData defaultColliderData;
    public SlopeData slopeData;

    public void CalculateCapsuleColliderDimensions()
    {
        SetCapsuleColliderRadius(defaultColliderData.Radius);
        SetCapsuleColliderHeight(defaultColliderData.Height * (1f - slopeData.StepHeightPercentage));
        RecalculateCapsuleColliderCenter();
    }

    private void RecalculateCapsuleColliderCenter()
    {
        float colliderHeightDiffrence = defaultColliderData.Height - capsuleColliderData.Collider.height;
        Vector3 newColliderCenter = new Vector3(0f, defaultColliderData.CenterY + (colliderHeightDiffrence / 2f), 0f);   
    }

    private void SetCapsuleColliderRadius(float radius)
    {
        capsuleColliderData.Collider.radius = radius;
    }
    
    private void SetCapsuleColliderHeight(float height)
    {
        capsuleColliderData.Collider.height = height;
    }
}
