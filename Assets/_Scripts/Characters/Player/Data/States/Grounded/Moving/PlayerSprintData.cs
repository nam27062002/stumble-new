using System;
using UnityEngine;

namespace MovementSystem
{
    [Serializable]
    public class PlayerSprintData
    {
        [Range(1f,3f)]  public float speedModifier = 1.7f;
        [Range(0f, 5f)] public float sprintToRunTime = 1f;
        [Range(0f,2f)] public float runToWalkTime = 0.5f;
    }
}