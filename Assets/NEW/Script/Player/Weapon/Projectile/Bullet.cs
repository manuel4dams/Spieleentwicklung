using UnityEngine;

namespace ScriptGG
{
    public class Bullet : Projectile
    {
        [Header("Bullet parameters")] //
        public float maxDistance;

        [Header("References")] //
        public LineRenderer lineRenderer;

        void Start()
        {
            var position = origin.position;
            var shootRay = new Ray
            {
                origin = position,
                direction = direction
            };
            lineRenderer.SetPosition(0, position);

            // Handle non hit
            if (!Physics.Raycast(shootRay, out var hit, maxDistance, hittableLayerMask.value))
            {
                lineRenderer.SetPosition(1, shootRay.origin + shootRay.direction * maxDistance);
                return;
            }

            lineRenderer.SetPosition(1, hit.point);

            // Handle hit
            var hittable = hit.collider.GetComponentInChildren<IHittable>();
            // Null propagation does not work with Unity
            // ReSharper disable once UseNullPropagation
            if (hittable != null)
                hittable.OnHit(damage);
        }
    }
}
