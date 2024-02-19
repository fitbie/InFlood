using System;
using System.Collections.Generic;
using InFlood.Entities.ActionSystem;
using UnityEngine;
using UnityEngine.Events;

namespace InFlood.Entities.Modules.Weapon
{
    /// <summary>
    /// Base class for all projectiles. Projectiles usen by ProjectileWeaponModule's. 
    /// NBallistic and Ballistic inherits from this class.
    /// </summary>
    public abstract class WeaponProjectile : MonoBehaviour, ICanDamage<DamageTypeValueWithEffect>
    {
        [field: SerializeField] public List<DamageTypeValueWithEffect> DamageTypes { get; set; }


        [Tooltip("UseGravity shoul be true for ballistic projectiles")]
        [SerializeField] protected Rigidbody rb;

        /// <summary>
        /// Weapon module projectile belongs to.
        /// </summary>
        protected ProjectileWeaponModule weapon;

        /// <summary>
        /// Called when after UnityEvents: hit IDamageable, fall in water or reach max distance.
        /// Also is using for callback so WeaponModule could disable current projectile.
        /// </summary>
        public event Action<WeaponProjectile> OnDieEvent;

        /// <summary>
        /// Called when projectile hitted some object.
        /// </summary>
        [SerializeField] protected  UnityEvent OnHit;


        /// <summary>
        /// Injects WeaponModule data to get FireRange/InitialSpeed/etc..
        /// </summary>
        /// <param name="owner">Weapon module projectile belongs to.</param>
        public virtual void Initialize(ProjectileWeaponModule owner)
        {
            weapon = owner;
        }

        public abstract void Shoot();


        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                ((ICanDamage<DamageTypeValueWithEffect>)this).Damage(damageable);
            }

            rb.velocity = Vector3.zero;

            OnHit?.Invoke();
            
            InvokeOnDieEvent();
        }
        

        protected void InvokeOnDieEvent()
        {
            OnDieEvent(this);
        }

    }

}