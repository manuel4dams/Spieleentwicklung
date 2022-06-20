using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    public float healthAmount = 1f;
    public AudioClip healthPickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player"))
            return;
        if (!other.GetComponent<PlayerHealth>().HealPlayer(healthAmount))
            return;

        AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, 0.4f);
        Destroy(transform.gameObject);
    }
}
