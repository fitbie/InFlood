using UnityEngine;
using UnityEngine.InputSystem;

namespace InFlood.InputSystem
{
    /// <summary>
    /// Provides reading Player's input for shooting. Should be on gameobject with PlayerInput component.
    /// </summary>
    [AddComponentMenu("Entities/Input/Player/PlayerFireInput")]
    public class PlayerFireInput : FireInput
    {
        [SerializeField] private PlayerInput playerInput;
        private InputAction fireAction;
        private InputAction rotateWeaponAction;


        private void Start()
        {
            Initialize();    
        }


        private void Initialize()
        {
            fireAction = playerInput.actions["Fire"];
            rotateWeaponAction = playerInput.actions["RotateWeapon"];
            // playerInput.actions["RotateWeapon"].performed += RotateWeapon;
            // playerInput.actions["RotateWeapon"].canceled += RotateWeapon;
        }


        // We use Update and not just subscribing to events bc we want FireInput to send fire events every frame,
        // so we don't need to use Update's in weapon classes.
        private void Update()
        {
            if (fireAction.IsPressed())
            {
                OnFireEventInvoke(true);
                // If we firing we also rotating weapon because it's the same button.
                OnRotateEventInvoke(rotateWeaponAction.ReadValue<Vector2>()); 
            }
            else if (fireAction.WasReleasedThisFrame())
            {
                OnFireEventInvoke(false);
                OnRotateEventInvoke(Vector2.zero);
            }
        }


        // public void Fire(InputAction.CallbackContext context)
        // {
        //     OnFireEventInvoke(context.ReadValue<float>() == 1);
        // }

        
        // public void RotateWeapon(InputAction.CallbackContext context)
        // {
        //     OnRotateEventInvoke(context.ReadValue<Vector2>());
        // }

    }

}