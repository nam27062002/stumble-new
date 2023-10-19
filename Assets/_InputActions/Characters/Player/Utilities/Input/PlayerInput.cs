using System;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerInput : MonoBehaviour
    {
        private InputActions _inputActions;
        public InputActions.PlayerActions PlayerActions;

        private void Awake()
        {
            _inputActions = new InputActions();
            PlayerActions = _inputActions.Player;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}

