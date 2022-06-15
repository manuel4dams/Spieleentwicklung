using UnityEngine;

public class AmmoPickupController : MonoBehaviour
{
    public int ammunitionAmount = 30;
    public AudioClip healthPickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;
        if (!other.GetComponentInChildren<Shoot>().RestockAmmunition(ammunitionAmount)) return;
        Destroy(transform.gameObject);
        AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, 0.4f);
    }
}
