using System;
using UnityEngine;

namespace MovementSystem
{
    [Serializable]
    public class PlayerRunData
    {
        [Range(1f,2f)] public float SpeedModidier = 1f; 
    }
}