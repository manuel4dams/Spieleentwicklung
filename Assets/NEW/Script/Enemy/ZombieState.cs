using GameGraph;
using MyBox;
using Project;
using UnityEngine;
using UnityEngine.UI;

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

        public AudioClip[] idleSounds;
        public AudioClip detectSound;
        public AudioClip[] hitSounds;
        public AudioClip attackSound;

        [Header("References")] //
        public Transform origin;
        public Vector3 Vector3Position;

        public SkinnedMeshRenderer skinnedMeshRenderer;
        public AudioSource audioSource;
        public Slider healthSlider;

        private bool healthSliderVisible;

        public void Start()
        {
            currentHealth = maxHealth;

            SetupMaterial();
        }
        
        public void Update()
        {
            Vector3Position = transform.position;
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
            if (!healthSliderVisible)
                ShowHealthSlider();

            Instantiate(zombieHitParticlesPrefab, origin.position + new Vector3(0, 1, 0), Quaternion.identity);

            PlayHitSound();

            currentHealth -= damage;

            healthSlider.value = currentHealth;

            if (currentHealth > 0)
                return;

            PlayDeathSound();
            RagDollDeath();
            Destroy(origin.gameObject);
        }

        private void ShowHealthSlider()
        {
            healthSlider.gameObject.SetActive(true);
            healthSliderVisible = true;
        }

        public void PlayDetectSound()
        {
            var tmpVolume = audioSource.volume;
            if (detectSound != null)
                return;
            audioSource.clip = detectSound;
            audioSource.volume = 3f;
            audioSource.Play();
            audioSource.volume = tmpVolume;
        }

        public void PlayHitSound()
        {
            if (hitSounds.IsNullOrEmpty())
                return;

            audioSource.clip = hitSounds[Random.Range(0, hitSounds.Length)];
            audioSource.Play();
        }

        public void PlayDeathSound()
        {
            MyAudioSource.PlayClipAtPoint(deathSound, transform.position, 2f);
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
