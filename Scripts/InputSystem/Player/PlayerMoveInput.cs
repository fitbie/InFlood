using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InFlood.InputSystem
{
    /// <summary>
    /// Provides reading Player's input for moving logic.
    /// </summary>
    [AddComponentMenu("Entities/Input/Player/PlayerMoveInput")]
    public class PlayerMoveInput : ShipMoveInput
    {
        [SerializeField] private PlayerInput playerInput;


        private void Start()
        {
            Initialize();
        }


        public void Initialize()
        {
            playerInput.actions["ChangeMoveModeKeyboard"].performed += ChangeMoveModeKeyboard;
            playerInput.actions["RotateShip"].performed += RotateShip;
            playerInput.actions["RotateShip"].canceled += RotateShip;

        }


        private void ChangeMoveModeKeyboard(InputAction.CallbackContext context)
        {
            ChangeMoveMode((int)context.ReadValue<float>());
        }


        private void RotateShip(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            Direction = new Vector3(value.x, 0, value.y);
        }

    }

}