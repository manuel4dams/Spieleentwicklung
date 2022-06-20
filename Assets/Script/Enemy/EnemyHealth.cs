using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maximumHealth;
    public float damageMultiplier;
    public GameObject damageParticlePrefab;
    public GameObject[] drops;
    public AudioClip deathSound;
    public bool burnable;
    public float burnDamage;
    public GameObject burnParticleGameObject;
    public float burnTime;
    public Slider healthSlider;
    private AudioSource enemySound;

    private bool onFire;
    private float nextBurn;
    private float endBurn;
    private float burnTickRate = 1f;

    private float currentHealth;

    void Start()
    {
        currentHealth = maximumHealth;
        healthSlider.maxValue = maximumHealth;
        healthSlider.value = currentHealth;

        enemySound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onFire && Time.time > nextBurn)
        {
            DamageEnemy(burnDamage);
            nextBurn += burnTickRate;
        }
        if (onFire && Time.time > endBurn)
        {
            onFire = false;
            burnParticleGameObject.SetActive(false);
        }
    }

    public void DamageEnemy(float damage)
    {
        healthSlider.gameObject.SetActive(true);
        damage = damage * damageMultiplier;
        if (damage <= 0) return;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        enemySound.Play();
        if (currentHealth <= 0)
        {
            KillEnemy();
        }
    }

    public void AddFire()
    {
        if (!burnable) return;
        onFire = true;
        burnParticleGameObject.SetActive(true);
        endBurn = Time.time + burnTime;
        nextBurn = Time.time + burnTickRate;
    }

    public void KillEnemy(bool forceKill = false)
    {
        var zombieController = GetComponentInChildren<ZombieController>();
        if (zombieController != null)
        {
            zombieController.RagDollDeath();
        }
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 2f);
        Destroy(gameObject.transform.root.gameObject);
        if (drops.Length == 0 || forceKill) return;
        foreach (var drop in drops)
        {
            var transform1 = transform;
            Instantiate(drop, transform1.position + Vector3.up, transform1.rotation);
        }
    }

    public void DamageFX(Vector3 point, Vector3 rotation)
    {
        Instantiate(damageParticlePrefab, point, Quaternion.identity);
    }
}
