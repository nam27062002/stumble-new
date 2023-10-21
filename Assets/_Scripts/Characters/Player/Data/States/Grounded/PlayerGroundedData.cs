using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MovementSystem
{
    [Serializable]
    public class PlayerGroundedData
    {
        [Range(0f,10f)] public float baseSpeed = 5f;
        public AnimationCurve slopeSpeedAngles;
        public PlayerRotationData baseRotationData;
        public PlayerWalkingData walkingData;
        public PlayerRunData runData;
        public PlayerDashData dashData;
        public PlayerSprintData sprintData;
        public PlayerStopData stopData;
    }
}