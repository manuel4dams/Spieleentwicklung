using Project;
using UnityEngine;

namespace ScriptGG
{
    public class Grenade : Projectile
    {
        [Header("Grenade parameters")] //
        public float speed;
        public float lifeTime;
        private float age;

        [Header("Grenade Audio")] //
        public AudioClip explosionSound;
        public float explosionLoudness = 1f;

        [Header("References")] //
        public new Rigidbody rigidbody;
        public GameObject explosion;

        void Start()
        {
            age = Time.realtimeSinceStartup + lifeTime;
            rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
        }

        private void Update()
        {
            if (Time.realtimeSinceStartup <= age)
                return;

            Debug.Log("Explode");
            Explode();
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

            Explode();
        }

        private void Explode()
        {
            var position = transform.position;

            // Spawn fire at impact location
            Instantiate(explosion, position, explosion.transform.rotation);
            MyAudioSource.PlayClipAtPoint(explosionSound, position, explosionLoudness);

            // Finally destroy the object, because we hit something
            Destroy(gameObject);
        }
    }
}
