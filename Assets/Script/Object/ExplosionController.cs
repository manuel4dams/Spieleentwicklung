// using UnityEngine;
//
// public class ExplosionController : MonoBehaviour
// {
//     // TODO burnable damage?
//
//     public Light explosionLight;
//     public float explosionForce;
//     public float explosionRadius;
//     public float damage;
//
//     void Start()
//     {
//         var explosionPosition = transform.position;
//         var colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
//         foreach (var collider in colliders)
//         {
//             var rigidbody = collider.GetComponent<Rigidbody>();
//             if (rigidbody != null)
//                 rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0.3f, ForceMode.Impulse);
//
//             if (collider.CompareTag("Player"))
//                 collider.gameObject.GetComponent<PlayerHealth>().DamagePlayer(damage);
//             else if (collider.CompareTag("Enemy"))
//                 collider.gameObject.GetComponent<EnemyHealth>().DamageEnemy(damage);
//         }
//     }
//
//     void Update()
//     {
//         explosionLight.intensity = Mathf.Lerp(explosionLight.intensity, 0f, 5 * Time.time);
//     }
// }
