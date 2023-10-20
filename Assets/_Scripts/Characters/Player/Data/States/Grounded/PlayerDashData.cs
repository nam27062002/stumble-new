using System;
using UnityEngine;
namespace MovementSystem
{
    [Serializable]
    public class PlayerDashData
    {
        [Range(1f, 3f)] public float speedModifier = 2f;
        [Range(1f, 2f)] public float timeTobeConsideredConsecutive = 1f;
        [Range(1, 10)] public int consecutiveDashesLimitAmount = 2;
        [Range(1f, 5f)] public float dashLimitReachedCooldown = 1.75f;
    }
}