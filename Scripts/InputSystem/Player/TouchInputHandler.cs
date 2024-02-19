using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace InFlood.InputSystem
{
    /// <summary>
    /// This helper class gets data from InputAction "Touch", handle it, and pass it.
    /// E.g. MoveController swipe, Camera zoom gestures, etc..
    /// </summary>
    public class TouchInputHandler : MonoBehaviour
    {
        #region Singleton

        private static TouchInputHandler instance;
        public static TouchInputHandler Instance
        {
            get => instance;
            set
            {
                if (instance == null)
                {
                    instance = value;
                }
                else
                {
                    Destroy(instance);
                    instance = value;
                }
            }
        }

        #endregion

        [SerializeField] private PlayerInput playerInput; // TODO: Get reference.
        [SerializeField] private PlayerMoveInput playerMoveInput; // TODO: Get reference.

        [Tooltip("Resolution we refer to. E.g. 100 pixels swipe on 1920*1080 is ~30 pixels on 3840*2100")]
        [SerializeField] private Vector2 referenceResolution;
        private float screenScaleFactor; // E.g. swipe length will be multiplied to this value.

        [SerializeField] private float swipeLength;


        private void Awake()
        {
            Instance = this;
            screenScaleFactor = (referenceResolution.x * referenceResolution.y) / (Screen.width * Screen.height);
        }


        private void OnEnable()
        {
            playerInput.actions["Touch"].performed += OnTouch;
        }


        private void OnTouch(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<TouchState>();
            
            # region Swipes

            if (playerInput.actions["RotateShip"].phase is InputActionPhase.Waiting &&
            Mathf.Abs(value.delta.y) > Mathf.Abs(value.delta.x) &&
            Mathf.Abs(value.delta.y) > swipeLength * screenScaleFactor &&
            value.phase is UnityEngine.InputSystem.TouchPhase.Ended)
            {
                Debug.Log(Math.Sign(value.delta.y));
                playerMoveInput.ChangeMoveMode(Math.Sign(value.delta.y));
            }

            #endregion
        }


        private void OnDisable()
        {
            playerInput.actions["Touch"].performed -= OnTouch;
        }
    }

}