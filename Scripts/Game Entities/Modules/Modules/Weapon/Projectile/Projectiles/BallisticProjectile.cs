using InFlood.Entities.ActionSystem;
using UnityEngine;
using UnityEngine.Events;

namespace InFlood.Entities.Modules.Weapon
{
    /// <summary>
    /// Ballistic version of WeaponProjectile. Rigidbody of this projectile must useGravity.
    /// Computing InitialSpeed via graviry and height.
    /// Destroys when fall in water or contact target.
    /// </summary>
    [AddComponentMenu("Entities/Modules/Weapon/Projectiles/BallisticProjectile")]
    public class BallisticProjectile : WeaponProjectile
    {
        private float initialSpeed;
        /// <summary>
        /// Called when projectile touch water's collider.
        /// </summary>
        [SerializeField] private UnityEvent OnFallInWater;


        public override void Initialize(ProjectileWeaponModule owner)
        {
            base.Initialize(owner);
            float h = transform.position.y;
            float g = Mathf.Abs(Physics.gravity.y);
            initialSpeed = owner.FireRange / Mathf.Sqrt(2 * h / g);
        }

        public override void Shoot()
        {
            rb.AddForce(transform.forward * initialSpeed, ForceMode.VelocityChange);
            transform.SetParent(null); // For independent moving.
        }


        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 4)
            {
                OnFallInWater?.Invoke();
            }
            else 
            {
                OnHit?.Invoke();

                if (other.TryGetComponent<IDamageable>(out var damageable))
                {
                    ((ICanDamage<DamageTypeValueWithEffect>)this).Damage(damageable);
                }
            }

            rb.velocity = Vector3.zero;

            InvokeOnDieEvent();
        }
    }

}