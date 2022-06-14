using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    public float healthAmount = 1f;
    public AudioClip healthPickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponent<PlayerHealth>().HealPlayer(healthAmount))
            {
                Destroy(transform.gameObject);
                AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, 0.4f);
            }
        }
    }
}
