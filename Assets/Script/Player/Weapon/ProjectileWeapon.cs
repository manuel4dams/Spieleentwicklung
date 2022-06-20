using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [Header("Projectile weapon settings")] //
    public GameObject projectilePrefab;

    protected override void FireImplementation(Transform weaponOrigin, Vector3 direction)
    {
        Instantiate(projectilePrefab, weaponOrigin.position, Quaternion.Euler(direction));
    }
}