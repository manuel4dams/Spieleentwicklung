using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    public float healthAmount = 1f;
    public AudioClip healthPickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;
        if (!other.GetComponent<PlayerHealth>().HealPlayer(healthAmount)) return;
        Destroy(transform.root.gameObject);
        AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, 0.4f);
    }
}
