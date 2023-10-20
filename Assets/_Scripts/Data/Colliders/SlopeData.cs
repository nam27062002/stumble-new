using System;
using UnityEngine;
namespace _Scripts.Data.Colliders
{
    [Serializable]
    public class SlopeData
    { 
        [Range(0f, 1f)] public float stepHeightPercentage = 0.25f;
        [Range(0f, 5f)] public float floatRayDistance = 2f;
        [Range(0f, 50f)] public float stepReachForce = 25f;
    }
}