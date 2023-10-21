using System;
using UnityEngine;

namespace MovementSystem
{
    [Serializable]
    public class PlayerStopData
    {
        [Range(0f, 15f)] public float lightDecelerationForce = 5f;
        [Range(0f, 15f)] public float mediumDecelerationForce = 6.5f;
        [Range(0f, 15f)] public float hardDecelerationForce = 5f;
    }
}