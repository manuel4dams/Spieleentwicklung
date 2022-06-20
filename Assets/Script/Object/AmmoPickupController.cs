using UnityEngine;

public class AmmoPickupController : MonoBehaviour
{
    public int ammunitionAmount = 30;
    public AudioClip healthPickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player"))
            return;
        if (!other.GetComponentInChildren<PlayerWeaponController>().RestockAmmunition(ammunitionAmount))
            return;

        AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, 0.4f);
        Destroy(transform.gameObject);
    }
}
