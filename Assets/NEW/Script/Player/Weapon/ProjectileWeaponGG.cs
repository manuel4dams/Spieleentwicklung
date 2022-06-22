using UnityEngine;

namespace ScriptGG
{
    public class ProjectileWeaponGG : WeaponGG
    {
        [Header("Projectile weapon settings")] //
        public GameObject projectilePrefab;

        protected override void FireImplementation(Transform weaponOrigin, Vector3 direction)
        {
            var go = Instantiate(projectilePrefab, weaponOrigin.position, Quaternion.Euler(direction));
            var projectile = go.GetComponent<Projectile>();
            projectile.origin = weaponOrigin;
            projectile.direction = direction;
            // TODO Damage will be in projectile settings
            //      And also speed, dropoff, distance, ...
        }
    }
}
