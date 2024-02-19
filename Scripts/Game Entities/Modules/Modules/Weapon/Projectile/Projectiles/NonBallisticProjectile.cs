using UnityEngine;
using UnityEngine.Events;

namespace InFlood.Entities.Modules.Weapon
{
    /// <summary>
    /// Non-Ballistic version of projectile (e.g. null-gravity Rigidbody, like Blaster).
    /// Use owner WeaponModule for get InitialSpeed and AddForce() it.  
    /// Checks distance in update and destroy when hit something, or when distance was passed.
    /// </summary>
    [AddComponentMenu("Entities/Modules/Weapon/Projectiles/NonBallisticProjectile")] 
    public class NonBallisticProjectile : WeaponProjectile
    {
        private Vector3 startPosition; // From where projectile was shooted. Used for FireRange tracking.

        [SerializeField] private UnityEvent OnReachMaxDistance; // When projectile reaches Weapon's FireRange. For FX and stuff.



        public override void Shoot()
        {
            startPosition = transform.position;
            rb.AddForce(transform.forward * weapon.InitialSpeed, ForceMode.VelocityChange);
            transform.SetParent(null); // For independent move.
        }


        private void Update()
        {
            if (Vector3.Distance(startPosition, transform.position) >= weapon.FireRange)
            {
                rb.velocity = Vector3.zero;
                OnReachMaxDistance?.Invoke();
                InvokeOnDieEvent();
            }
        }
    }

}