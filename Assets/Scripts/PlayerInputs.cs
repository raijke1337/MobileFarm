using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Farm.GameManager;

namespace Farm.Game.Inputs
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private InputAction _move;
        [SerializeField] private InputAction _click;

        [Space, SerializeField, Range(1, 10)] private float _interactRange = 1.4f;

        public event SimpleEvent ClickEvent;
        public event SimpleEvent MoveEvent;
        public float InteractRange { get => _interactRange; }
        

        private void OnEnable()
        {
            _move.Enable();
            _click.Enable();
            _move.performed += Move_performed;
            _click.performed += Click_performed;
        }
        private void OnDisable()
        {
            _move.performed -= Move_performed;
            _click.performed -= Click_performed;
        }

        private void Move_performed(InputAction.CallbackContext obj) => MoveEvent?.Invoke();
        private void Click_performed(InputAction.CallbackContext obj) => ClickEvent?.Invoke();

    }

    
}