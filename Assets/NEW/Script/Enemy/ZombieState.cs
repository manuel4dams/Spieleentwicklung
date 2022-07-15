using System;
using GameGraph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScriptGG
{
    [GameGraph]
    public class ZombieState : MonoBehaviour, IHittable
    {
        [Header("Health")] //
        public float maxHealth = 10f;
        public float currentHealth;

        [Header("Movement parameters")] //
        public float runSpeedMultiplier = 2f;
        public float walkSpeedMultiplier = 1f;
        public float turnSpeed = 20;
        public float idleWaitTime;
        public Vector3 movementBoundsLeft;
        public Vector3 movementBoundsRight;

        [Header("Damage")] //
        public float damage;
        public float attackRate;

        [Header("Visual")] //
        public Material[] materials;
        public Material material;
        public GameObject zombieHitParticlesPrefab;
        public GameObject ragDollDead;

        [Header("Audio")] //
        public AudioClip deathSound;

        [Header("References")] //
        public Transform origin;
        public SkinnedMeshRenderer skinnedMeshRenderer;

        void Start()
        {
            currentHealth = maxHealth;

            SetupMaterial();
        }

        private void SetupMaterial()
        {
            if (!material)
                material = materials[Random.Range(0, materials.Length)];

            skinnedMeshRenderer.material = material;
        }

        public void OnHit(float damage)
        {
            // TODO use event and gamegraph to handle the hit
            // all quick fix for now maybe get the actual hit position later
            Instantiate(zombieHitParticlesPrefab, origin.position + new Vector3(0, 1, 0), Quaternion.identity);
            currentHealth -= damage;

            if (currentHealth > 0)
                return;

            AudioSource.PlayClipAtPoint(deathSound, transform.position, 2f);
            RagDollDeath();
            Destroy(origin.gameObject);
        }

        private void RagDollDeath()
        {
            // TODO handle in Controller with gamegraph
            var ragDollModel = Instantiate(ragDollDead, transform.position, Quaternion.identity) as GameObject;
            var ragDollJoints = origin.Find("master").GetComponentsInChildren<Transform>();
            var zombieJoints = ragDollModel.transform.Find("master").GetComponentsInChildren<Transform>();

            foreach (var ragDollJoint in ragDollJoints)
            {
                foreach (var zombieJoint in zombieJoints)
                {
                    if (zombieJoint.name != ragDollJoint.name) continue;
                    ragDollJoint.position = zombieJoint.position;
                    ragDollJoint.rotation = zombieJoint.rotation;
                    break;
                }
            }
            ragDollModel.transform.rotation = origin.transform.Find("master").rotation;

            ragDollModel.transform.Find("master").GetComponent<Renderer>().material = material;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(movementBoundsLeft + Vector3.up, movementBoundsRight + Vector3.up);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(movementBoundsLeft + Vector3.up * 2f, movementBoundsLeft);
            Gizmos.DrawLine(movementBoundsRight + Vector3.up * 2f, movementBoundsRight);
        }
    }
}
