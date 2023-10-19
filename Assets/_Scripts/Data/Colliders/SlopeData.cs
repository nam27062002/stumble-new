using System;
using UnityEngine;

namespace _Scripts.Data.Colliders
{
    [Serializable]
    public class SlopeData
    {
        [Range(0f, 1f)] public float StepHeightPercentage = 0.25f;
        
    }
}