using UnityEngine;

public class HitScanWeapon : Weapon
{
    [Header("Weapon settings")] //
    public float maximalTravelDistance;
    public LayerMask layerMask;

    [Header("References")] //
    public LineRenderer gunLineRenderer;

    protected override void FireImplementation(Transform weaponOrigin, Vector3 direction)
    {
        var position = weaponOrigin.position;
        var shootRay = new Ray
        {
            origin = position,
            direction = direction
        };
        gunLineRenderer.SetPosition(0, position);

        // Handle non hit
        if (!Physics.Raycast(shootRay, out var targetHit, maximalTravelDistance, layerMask.value))
        {
            gunLineRenderer.SetPosition(1, shootRay.origin + shootRay.direction * maximalTravelDistance);
            return;
        }

        // Handle hit
        gunLineRenderer.SetPosition(1, targetHit.point);
        switch (targetHit.collider.tag)
        {
            case "Enemy":
                var enemyHealth = targetHit.collider.GetComponent<EnemyHealth>();
                enemyHealth.DamageEnemy(damage);
                enemyHealth.DamageFX(targetHit.point, -shootRay.direction);
                break;
            // TODO
            case "Crate":
                targetHit.collider.GetComponent<Object>().Hit();
                break;
            case "Barrel":
                targetHit.collider.GetComponent<Object>().Hit();
                break;
        }
    }
}