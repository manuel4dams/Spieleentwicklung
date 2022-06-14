using UnityEngine;

public class AmmoPickupController : MonoBehaviour
{
    public int ammunitionAmount = 30;
    public AudioClip healthPickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponentInChildren<Shoot>().RestockAmmunition(ammunitionAmount))
            {
                Destroy(transform.gameObject);
                AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, 0.4f);
            }
        }
    }
}
