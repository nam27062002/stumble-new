using UnityEngine;

namespace Characters.Config.Movement
{
    public class MovementConfig : ScriptableObject
    {
        [field: SerializeField] [field: Range(0f, 25f)] public float MoveSpeed { get; private set; } = 7f;
        [field: SerializeField] [field: Range(0f, 25f)] public float GroundMultiplier { get; private set; } = 10f;
        [field: SerializeField] [field: Range(0f, 25f)] public float GroundDrag { get; private set; } = 5f;
        [field: SerializeField] [field: Range(0f, 25f)] public float JumpForce { get; private set; } = 12f;
        [field: SerializeField] [field: Range(0f, 1f)] public float JumpCooldown { get; private set; } = 0.25f;
        [field: SerializeField] [field: Range(0f, 1f)] public float AirMultiplier { get; private set; } = 0.4f;
    }
}