using UnityEngine;

namespace ScriptGG
{
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile parameters")] //
        public float damage;
        public LayerMask hittableLayerMask;

        // Given data
        public Transform origin { get; set; }
        public Vector3 direction { get; set; }
    }
}
