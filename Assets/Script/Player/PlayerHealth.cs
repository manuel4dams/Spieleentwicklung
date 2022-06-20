using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth: MonoBehaviour
{
    private const float PLAYER_DAMAGE_INDICATOR_FADE_SPEED = 5f;

    [Header("Health")] //
    public float maxHealth = 10f;
    public bool godMode;
    [ReadOnly] [SerializeField] private float currentHealth;

    [Header("References")] //
    public AudioSource playerAudioSource;
    public GameObject playerDeathParticlePrefab;

    // GUI
    public Slider playerHealthSlider;
    public Image playerDamageIndicatorImage;
    public Color playerDamageIndicatorFlashColor = new(255f, 255f, 255f, 1f);

    private bool playerIsDamaged;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = currentHealth;

        playerDamageIndicatorImage.color = playerIsDamaged
            ? playerDamageIndicatorFlashColor
            : Color.Lerp(playerDamageIndicatorImage.color, Color.clear, PLAYER_DAMAGE_INDICATOR_FADE_SPEED);
        playerIsDamaged = false;
    }

    public void DamagePlayer(float damage)
    {
        // Prevent negative life pool while god mode is turned on and for general safety
        // TODO Fix comment or logic, since it does not match this way
        if (currentHealth > 0f)
            currentHealth -= damage;

        playerIsDamaged = true;
        playerAudioSource.Play();

        if (currentHealth > 0)
            return;

        playerDamageIndicatorImage.color = playerDamageIndicatorFlashColor;
        KillPlayer();
    }

    /// <returns>False if at max health</returns>
    public bool HealPlayer(float healing)
    {
        // If at max health, return false because no healing was applied
        if (currentHealth >= maxHealth)
            return false;

        currentHealth += healing;

        // Limit health to max health
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        playerHealthSlider.value = currentHealth;

        // Healing was applied
        return true;
    }

    public void KillPlayer(bool forceKill = false)
    {
        if (godMode && !forceKill)
            return;

        Instantiate(playerDeathParticlePrefab, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }
}
