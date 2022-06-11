using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public bool godMode;
    private float currentHealth;

    public GameObject playerDeathFX;

    // GUI
    public Slider playerHealthSlider;
    public Image playerDamageIndicatorImage;
    private Color playerDamageIndicatorFlashColor = new Color(255f, 255f, 255f, 1f);
    private float playerDamageIndicatorFadeSpeed = 5f;
    private bool playerIsDamaged = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = currentHealth;
    }

    private void Update()
    {
        if (playerIsDamaged)
        {
            playerDamageIndicatorImage.color = playerDamageIndicatorFlashColor;
        }
        else
        {
            playerDamageIndicatorImage.color = Color.Lerp(playerDamageIndicatorImage.color, Color.clear, playerDamageIndicatorFadeSpeed);
        }
        playerIsDamaged = false;
    }

    public void DamagePlayer(float damage)
    {
        // prevent negative lifepool in godmode
        if (currentHealth > 0f)
            currentHealth -= damage;

        playerHealthSlider.value = currentHealth;
        playerIsDamaged = true;

        if (currentHealth <= 0)
        {
            playerDamageIndicatorImage.color = playerDamageIndicatorFlashColor;
            KillPlayer();
        }
    }

    // return false if player can't be healed further
    public bool HealPlayer(float healing)
    {
        // already max health or above 
        if (currentHealth >= maxHealth)
        {
            return false;
        }
        
        currentHealth += healing;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        playerHealthSlider.value = currentHealth;
        return true;
    }

    public void KillPlayer(bool forceKill = false)
    {
        if (godMode && !forceKill) return;
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }
}
