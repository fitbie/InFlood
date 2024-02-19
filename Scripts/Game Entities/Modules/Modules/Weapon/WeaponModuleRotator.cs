using UnityEngine;

namespace InFlood.Entities.Modules.Weapon
{
    /// <summary>
    /// This class subscribe to Entitie's IFireInput and handles rotation logic of WeaponModule.
    /// Works when "rotatable" bool checked on WeaponModule.
    /// </summary> <summary>
    [AddComponentMenu("Entities/Modules/Weapon/WeaponRotator")]
    [RequireComponent(typeof(WeaponModule))]
    public class WeaponModuleRotator : MonoBehaviour
    {
        [SerializeField] private Transform rotatablePart;
        private float CurrentAngle // Y rotation of rotatable part.
        {
            get => rotatablePart.eulerAngles.y;
            set
            {
                rotatablePart.eulerAngles = new Vector3(0, value, 0);
            }
        }
        [SerializeField] private float rotationSpeed;
        [Tooltip("Max rotation angle. E.g. if its 180 - weapon can rotate 90 to left and 90 ro right")]
        
        /// <summary>
        /// Angle of FireInput direction (e.g. Gamepad stick for player, etc.)
        /// </summary>
        private float targetAngle;

        /// <summary>
        /// Diff between current and target(FireInput) angles.
        /// </summary>
        private float deltaAngle = 0;

        /// <summary>
        /// Is weapon currently fully rotated towards input?
        /// Using delta between current and target angles with little error value (1).
        /// </summary>
        /// <value></value>
        public bool IsFullyRotated 
        { 
            get => Mathf.Abs(deltaAngle) < 5;
        }


        /* TODO: Possible weapon max/min rotation angle
        public void Initialize()
        {
            minRotationAngle = CurrentAngle - rotationAngle / 2;
            maxRotationAngle = CurrentAngle + rotationAngle / 2;
        }
        */


        /// <summary>
        /// Rotates weapon towards gamepad stick. Works only when you check "rotatable" bool & specify rotatablePart.
        /// </summary>
        /// <param name="direction">Gamepad stick direction</param>
        public void RotateWeapon(Vector2 direction)
        {
            if (direction == Vector2.zero) 
            {
                deltaAngle = 0; // Reset delta in case of shooting by tap fire button for player.
                return;
            }
            else
            {
                targetAngle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
    
                deltaAngle = Mathf.DeltaAngle(CurrentAngle, targetAngle);

                // TODO!: Fix every frame interpolation. E.g. if RotationSpeed is 0.5, While RotateWeapon() - delta is
                // divided by 2 every frame, so it never gonna reach target value. 
                var angle = CurrentAngle + (deltaAngle * rotationSpeed);
                CurrentAngle = angle;
            }
            
        }

    }

}