using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
        
        public void DisableActionFor(InputAction action, float seconds)
        {
            StartCoroutine(DisableAction(action, seconds));
        }

        private IEnumerator DisableAction(InputAction action, float seconds)
        {
            action.Disable();
            yield return new WaitForSeconds(seconds);
            action.Enable();
        }
    }
}

