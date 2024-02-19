using System.Collections.Generic;
using UnityEngine;

namespace InFlood.Entities.Modules.Weapon
{
    [AddComponentMenu("Entities/Modules/Weapon/ProjectileWeaponModule")]
    public class ProjectileWeaponModule : WeaponModule
    {
        [field: SerializeField] public override float FireRange { get; protected set; }
        [field: SerializeField] public override float FireRate { get; protected set; }
        [field: SerializeField] public override float InitialSpeed { get; set; }

        [field: SerializeField] public WeaponProjectile Projectile { get; protected set; }

        [SerializeField] private Transform projectileSpawnpoint;
        private Stack<WeaponProjectile> pool = new();
        private float timer;


        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);
        }


        protected override void Fire(bool status)
        {
            if(!status || Time.time < timer || !CanShoot()) { return; }

            OnFire?.Invoke();

            SpawnProjectile();
            timer = Time.time + 1 / FireRate;
        }


        private void SpawnProjectile()
        {
            if (!pool.TryPop(out var projectile))
            {
                projectile = Instantiate(Projectile, projectileSpawnpoint.position, projectileSpawnpoint.rotation, projectileSpawnpoint);
                projectile.Initialize(this);
                projectile.OnDieEvent += OnProjectileDie;
            }

            projectile.gameObject.SetActive(true);
            projectile.Shoot();
        }


        private void OnProjectileDie(WeaponProjectile projectile)
        {
            projectile.gameObject.SetActive(false);
            projectile.transform.SetPositionAndRotation(projectileSpawnpoint.position, projectileSpawnpoint.rotation);
            projectile.transform.SetParent(projectileSpawnpoint);
            pool.Push(projectile);
        }
    }

}