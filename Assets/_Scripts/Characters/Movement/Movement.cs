using Characters.Config.Movement;
using UnityEngine;

namespace Characters.Movement
{
    public class Movement : MonoBehaviour
    {
        // [SerializeField] private MovementConfig movementConfig;
        // [SerializeField] private Transform orientation;
        // private Vector2 _direction = Vector2.zero;
        // private Rigidbody _rb;
        // private Vector3 _moveDirection;
        // private void Start()
        // {
        //     _rb = transform.parent.GetComponent<Rigidbody>();
        //     _rb.freezeRotation = true;
        // }
        //
        // private void Update()
        // {
        //     SpeedControl();
        //     GetInput();
        //     _rb.drag = movementConfig.GroundDrag;
        // }
        //
        // private void FixedUpdate()
        // {
        //     MovePlayer();
        // }
        //
        // private void GetInput()
        // {
        //     _direction = InputManager.Instance.Direction;
        // }
        //
        // private void MovePlayer()
        // {
        //     _moveDirection = orientation.forward * _direction.y + orientation.right * _direction.x;
        //     _rb.AddForce(_moveDirection.normalized * (movementConfig.MoveSpeed * movementConfig.GroundMultiplier), ForceMode.Force);
        // }
        //
        // private void SpeedControl()
        // {
        //     Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        //     
        //     if(flatVel.magnitude > movementConfig.MoveSpeed)
        //     {
        //         Vector3 limitedVel = flatVel.normalized * movementConfig.MoveSpeed;
        //         _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        //     }
        // }
    }
}