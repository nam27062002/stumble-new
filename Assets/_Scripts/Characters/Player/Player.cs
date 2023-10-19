using UnityEngine;
using UnityEngine.Serialization;

namespace MovementSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        public PlayerInput input;
        private Rigidbody _rigidbody;
        private Transform _mainCameraTransform;
        private PlayerMovementStateMachine _movementStateMachine;
        private void Awake()
        {
            
            input = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody>();
            if (Camera.main != null) _mainCameraTransform = Camera.main.transform;
            _movementStateMachine = new PlayerMovementStateMachine(this);
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
        
        public Vector2 GetMovementInput => input.PlayerActions.Move.ReadValue<Vector2>();

        public void SetAddForce(Vector3 movementDir)
        {
            _rigidbody.AddForce(movementDir, ForceMode.VelocityChange);
        }

        public void SetRotation(Quaternion quaternion)
        {
            _rigidbody.MoveRotation(quaternion);
        }
        public Vector3 GetEulerAnglesCamera => _mainCameraTransform.eulerAngles;
        public Vector3 GetEulerAnglesPlayer => _rigidbody.rotation.eulerAngles;
        public Vector3 PlayerHorizontalVelocity
        {
            get { return _rigidbody.velocity; }
            set { _rigidbody.velocity = value; }
        }

    }
}