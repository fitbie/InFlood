using InFlood.Entities.ActionSystem;
using InFlood.Entities.Modules.Weapon;
using InFlood.InputSystem;
using UnityEngine;
using UnityEngine.Events;

namespace InFlood.Entities.Modules
{
    /// <summary>
    /// Weapon Module subscribe to FireInput to tell Shooter class to emit damage units (e.g. bullets).
    /// Can be rotatable and static, respectively rotate and fire everywhere or stay and check for target before fire. 
    /// </summary>
    public abstract class WeaponModule : Module
    {
        /// <summary>
        /// How far projectiles/particles/etc. can fly.
        /// </summary>
        public abstract float FireRange { get; protected set; }
        
        /// <summary>
        /// Projectiles/particles/etc. per 1 second.
        /// </summary>
        public abstract float FireRate { get; protected set; }

        /// <summary>
        /// Initial speed of projectiles. Be aware that ballistick projectiles doesn't use this property.
        /// </summary>
        public abstract float InitialSpeed { get; set; }

        [Tooltip("Raised when weapon is shooting")]
        [SerializeField] protected UnityEvent OnFire;

        [Tooltip("Is Weapon rotatable? If true - weapon rotates towards Fire Gamepad Stick, if false - weapon is static and raycasts for possible enemies before shooting")]
        [SerializeField] protected bool rotatable;
        [SerializeField] private WeaponModuleRotator rotator;



        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);

            if (owner is not IFireInput fireInterface) 
            {
                throw new InitializeModuleException($"You're trying to initialize Weapon Module on non-IFireInput object {owner.gameObject.name}!", this);
            }

            fireInterface.FireInput.OnFireEvent += Fire;
            
            if (rotatable) { fireInterface.FireInput.OnRotateEvent += rotator.RotateWeapon; }
            
            // if (this is IRotatableWeapon rotatableWeapon) 
            // { 
            //     fireInterface.FireInput.OnRotateEvent += rotatableWeapon.RotateWeapon;
            // }
        }


        protected abstract void Fire(bool status);


        protected bool CanShoot()
        {
            if (!rotatable) { return HasTarget(); }
            else { return rotator.IsFullyRotated; }
        }


        /// <summary>
        /// Non-rotatable Weapons can only shoot when there is target. So we use raycast for target checking and
        /// return if raycast hit something && is hitted object implementing IDamageable.
        /// </summary>
        protected bool HasTarget()
        {
            return Physics.Raycast(transform.position, transform.forward, out var hit, FireRange) &&
            hit.collider.TryGetComponent<IDamageable>(out var _);
        }


        public override void Terminate()
        {
            base.Terminate();
            Fire(false);

            if (Owner is IFireInput fireInterface) 
            { 
                fireInterface.FireInput.OnFireEvent -= Fire;
                if (rotatable) { fireInterface.FireInput.OnRotateEvent -= rotator.RotateWeapon; }

                // if (this is IRotatableWeapon rotatableWeapon) 
                // { 
                //     fireInterface.FireInput.OnRotateEvent -= rotatableWeapon.RotateWeapon;
                // }
            }
        }

    }

}
