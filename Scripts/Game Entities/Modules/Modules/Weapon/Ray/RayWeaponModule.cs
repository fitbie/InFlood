using System.Collections.Generic;
using InFlood.Entities.ActionSystem;
using UnityEngine;
using UnityEngine.Events;

namespace InFlood.Entities.Modules.Weapon
{
    [AddComponentMenu("Entities/Modules/Weapon/RayWeaponModule")]
    public class RayWeaponModule : WeaponModule, ICanDamage<DamageTypeValueWithEffect>
    {
        [SerializeField] private LineRenderer rayRenderer;
        [SerializeField] private Transform rayOrigin;

        [field: SerializeField] public override float FireRange { get; protected set; }

        /// <summary>
        /// In case of Ray FireRate is "hits" per second when ray touch any IDamageable object.
        /// </summary>
        [field: SerializeField] public override float FireRate { get; protected set; }
        public override float InitialSpeed { get; set; } // TODO: Weapon Ray from point - to point speed.

        [field: SerializeField] public List<DamageTypeValueWithEffect> DamageTypes { get; set; }
        
        // Invokes when ray collides with something and FireRate is passed. Used for example - FX like sparks and stuff.
        public UnityEvent<Vector3> OnRayCollides; 
        private float timer = 0f; // Shoot counter for FireRate.

        protected override void Fire(bool status)
        {
            if (status && CanShoot())
            {
                OnFire?.Invoke();
                CastRay();
            }
            else
            {
                ResetRay();
            }
        }


        private void CastRay()
        {
            Vector3 endPoint; // Where ray ends.
            
            // Set ray point to hitted collider.
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out var ray, FireRange, ~(1 << Owner.gameObject.layer)))
            {
                endPoint = ray.point;

                OnRayCollides?.Invoke(endPoint);

                if (timer < Time.time && ray.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    ((ICanDamage<DamageTypeValueWithEffect>)this).Damage(damageable);
                    timer = Time.time + 1 / FireRate;
                }
            }
            // Or set ray point to fire range from origin.
            else
            {
                endPoint = rayOrigin.position + (rayOrigin.forward * FireRange);
            }
            
            rayRenderer.positionCount = 2;
            rayRenderer.SetPosition(0, rayOrigin.position);
            rayRenderer.SetPosition(1, endPoint);
        }


        private void ResetRay()
        {
            rayRenderer.positionCount = 0;
        }
    }

}