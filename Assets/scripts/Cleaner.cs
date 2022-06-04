using System;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                playerHealth.KillPlayer();
                break;

            default:
                Destroy(other.gameObject);
                break;
        }
    }
}
