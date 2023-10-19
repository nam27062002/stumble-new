using System;
using UnityEngine;

namespace MovementSystem
{
    [Serializable]
    public class PlayerWalkingData
    {
        [Range(0f,1f)] public float SpeedModidier = 0.225f; 
    }
}