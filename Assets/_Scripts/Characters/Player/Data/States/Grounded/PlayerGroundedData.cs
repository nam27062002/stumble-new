﻿using System;
using UnityEngine;

namespace MovementSystem
{
    [Serializable]
    public class PlayerGroundedData
    {
        [Range(0f,10f)] public float baseSpeed = 5f;
        public AnimationCurve SlopeSpeedAngles;
        public PlayerRotationData BaseRotationData;
        public PlayerWalkingData WalkingData;
        public PlayerRunData RunData;
        public PlayerDashData DashData;
    }
}