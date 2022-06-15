using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maximalTravelDistance;
    public float damage;

    private Ray shootRay;
    private RaycastHit targetHit;
    private int layerMask;
    private LineRenderer gunLine;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();

        var transform1 = transform;
        var position = transform1.position;
        shootRay.origin = position;
        shootRay.direction = transform1.forward;
        gunLine.SetPosition(0, position);

        if (Physics.Raycast(shootRay, out targetHit, maximalTravelDistance, layerMask))
        {
            switch (targetHit.collider.tag)
            {
                case "Enemy":
                    var enemyHealth = targetHit.collider.GetComponent<EnemyHealth>();
                    enemyHealth.DamageEnemy(damage);
                    enemyHealth.DamageFX(targetHit.point, -shootRay.direction);
                    break;
                case "Crate":
                    targetHit.collider.GetComponent<Crate>().DestroyCrate();
                    break;
                case "Barrel":
                    targetHit.collider.GetComponent<Barrel>().HitBarrel();
                    break;
            }
            gunLine.SetPosition(1, targetHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * maximalTravelDistance);
        }
    }
}
