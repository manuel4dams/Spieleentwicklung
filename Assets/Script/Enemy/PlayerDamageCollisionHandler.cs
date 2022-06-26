// using UnityEngine;
//
// public class PlayerDamageCollisionHandler : MonoBehaviour
// {
//     public float damage;
//     public float damageRate;
//     public float pushBackForce;
//
//     private float timeNextDamageAllowed;
//     private bool playerInRange;
//
//     private GameObject player;
//     private PlayerHealth playerHealth;
//
//     void Start()
//     {
//         timeNextDamageAllowed = Time.time;
//         player = GameObject.FindGameObjectWithTag("Player");
//         playerHealth = player.GetComponent<PlayerHealth>();
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (playerInRange)
//             Attack();
//     }
//
//     // TODO may be obsolete workaround
//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerInRange = true;
//         }
//     }
//
//     // TODO may be obsolete workaround
//     void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerInRange = false;
//         }
//     }
//
//     private void Attack()
//     {
//         if (timeNextDamageAllowed > Time.time)
//             return;
//
//         playerHealth.DamagePlayer(damage);
//         timeNextDamageAllowed = Time.time + damageRate;
//
//         PushBack(player.transform);
//     }
//
//     private void PushBack(Transform pushedObject)
//     {
//         // TODO Only up is a bit strange
//         var pushDirection = new Vector3(0, pushedObject.position.y - transform.position.y, 0);
//         pushDirection *= pushBackForce;
//
//         var pushedObjectRigidBody = pushedObject.GetComponent<Rigidbody>();
//         pushedObjectRigidBody.velocity = Vector3.zero;
//         pushedObjectRigidBody.AddForce(pushDirection, ForceMode.Impulse);
//     }
// }
