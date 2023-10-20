using System;
using System.Collections;
using _Scripts.Utilities.Colliders;
using MovementSystem.Data.Layers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace MovementSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [Header("References")]
        public PlayerSO data;
        [Header("Collisions")]
        public CapsuleColliderUtility colliderUtility;
        public PlayerLayerData layerData;
        [Header("Model")]
        public GameObject modelPrefab;
        
        [NonSerialized] public PlayerInput Input;
        
        [NonSerialized] public Rigidbody Rigidbody;
        private Transform _mainCameraTransform;
        private PlayerMovementStateMachine _movementStateMachine;
        private void Awake()
        {
            Input = GetComponent<PlayerInput>();
            Rigidbody = GetComponent<Rigidbody>();
            if (Camera.main != null) _mainCameraTransform = Camera.main.transform;
            _movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void OnValidate()
        {
            colliderUtility.Initialize(modelPrefab.GetComponent<CapsuleCollider>());
            colliderUtility.CalculateCapsuleColliderDimensions();
        }

        private void Start()
        {
            _movementStateMachine.ChangeState(_movementStateMachine.IdlingState);
        }

        private void Update()
        {
            _movementStateMachine.HandleInput();
            _movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _movementStateMachine.PhysicsUpdate();
        }
        public Vector2 GetMovementInput => Input.PlayerActions.Move.ReadValue<Vector2>();

        public void SetAddForce(Vector3 movementDir)
        {
            Rigidbody.AddForce(movementDir, ForceMode.VelocityChange);
        }

        public void SetRotation(Quaternion quaternion)
        {
            Rigidbody.MoveRotation(quaternion);
        }
        public Vector3 GetEulerAnglesCamera => _mainCameraTransform.eulerAngles;
        public Vector3 GetEulerAnglesPlayer => Rigidbody.rotation.eulerAngles;
        public Vector3 PlayerHorizontalVelocity
        {
            get { return Rigidbody.velocity; }
            set { Rigidbody.velocity = value; }
        }

    }
}