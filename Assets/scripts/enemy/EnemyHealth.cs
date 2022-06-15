using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maximumHealth;
    public float damageMultiplier;
    public GameObject damageParticles;
    public GameObject[] drops;
    public AudioClip deathSound;
    public bool burnable;
    public float burnDamage;
    public GameObject burnFX;
    public Slider healthSlider;
    private AudioSource enemySound;

    private bool onFire;
    public float burnTime;
    private float nextBurn;
    private float endBurn;
    private float burnTickRate = 1f;

    private float currentHealth;

    // Start is called before the first frame update
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
            burnFX.SetActive(false);
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
            Kill();
        }
    }

    public void AddFire()
    {
        if (!burnable) return;
        onFire = true;
        burnFX.SetActive(true);
        endBurn = Time.time + burnTime;
        nextBurn = Time.time + burnTickRate;
    }

    private void Kill()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 2f);
        Destroy(gameObject.transform.root.gameObject);
        if (drops.Length == 0) return;
        foreach (var drop in drops)
        {
            var transform1 = transform;
            Instantiate(drop, transform1.position + Vector3.up, transform1.rotation);
        }
    }

    public void DamageFX(Vector3 point, Vector3 rotation)
    {
        Instantiate(damageParticles, point, Quaternion.Euler(rotation));
    }
}
