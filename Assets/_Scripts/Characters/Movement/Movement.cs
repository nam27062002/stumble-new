using _Scripts.Initialization;
using Characters.Config.Movement;
using InputSystem;
using UnityEngine;

namespace Characters.Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Transform _orientation;
        private MovementConfig _movementConfig;
        private Vector2 _direction = Vector2.zero;
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private void Start()
        {
            _rb = transform.parent.GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _movementConfig =  ConfigInstaller.Instance.MovementConfig;
        }
        
        private void Update()
        {
            SpeedControl();
            GetInput();
            _rb.drag = _movementConfig.GroundDrag;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void GetInput()
        {
            _direction = InputManager.Instance.Direction;
        }
        
        private void MovePlayer()
        {
            _moveDirection = _orientation.forward * _direction.y + _orientation.right * _direction.x;
            _rb.AddForce(_moveDirection.normalized * _movementConfig.MoveSpeed * _movementConfig.GroundMultiplier, ForceMode.Force);
        }
        
        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            
            if(flatVel.magnitude > _movementConfig.MoveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * _movementConfig.MoveSpeed;
                _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
            }
        }
    }
}