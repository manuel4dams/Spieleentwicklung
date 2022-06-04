using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject playerDeathFX;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    public void HealPlayer(float healing)
    {
        currentHealth += healing;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void KillPlayer()
    {
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }
}
