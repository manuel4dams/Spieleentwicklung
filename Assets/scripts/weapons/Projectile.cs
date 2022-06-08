using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maximalTravelDistance = 10f;
    public float damage;

    private Ray shootRay;
    private RaycastHit targetHit;
    private int layerMask;
    private LineRenderer gunLine;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, transform.position);

        if (Physics.Raycast(shootRay, out targetHit, maximalTravelDistance, layerMask))
        {
            gunLine.SetPosition(1, targetHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * maximalTravelDistance);
        }
    }
}
