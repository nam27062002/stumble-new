using System;
using Extensions;
using UnityEngine;

namespace InputSystem
{
    public class InputManager : Singleton<InputManager>
    {
        private InputActions _inputActions;
        private InputActions.GameplayActions _gameplayActions;
        public Vector2 Direction => _gameplayActions.Move.ReadValue<Vector2>();
        protected override void Awake()
        {
            base.Awake();
            _inputActions = new InputActions();
            _gameplayActions = _inputActions.Gameplay;
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