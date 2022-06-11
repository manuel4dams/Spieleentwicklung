using UnityEngine;

public class AmmoPickupController : MonoBehaviour
{
    public int ammunitionAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponentInChildren<Shoot>().RestockAmmunition(ammunitionAmount))
            {
                Destroy(transform.root.gameObject);
            }
        }
    }
}
