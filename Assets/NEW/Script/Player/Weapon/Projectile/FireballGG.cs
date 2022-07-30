using UnityEngine;

namespace ScriptGG
{
    public class FireballGG : Projectile
    {
        [Header("Fireball parameters")] //
        public float speed;

        [Header("References")] //
        public new Rigidbody rigidbody;
        public GameObject fire;

        void Start()
        {
            rigidbody.AddForce(direction * speed, ForceMode.Impulse);
        }

        void OnTriggerEnter(Collider other)
        {
            if (!LayerMaskHelper.LayerIsInMask(other.gameObject.layer, hittableLayerMask))
                return;

            var hittable = other.GetComponentInChildren<IHittable>();
            // Null propagation does not work with Unity
            // ReSharper disable once UseNullPropagation
            if (hittable != null)
                hittable.OnHit(damage);

            // Spawn fire at impact location
            Instantiate(fire, transform.position, fire.transform.rotation);

            // Finally destroy the object, because we hit something
            Destroy(gameObject);
        }
    }
}
