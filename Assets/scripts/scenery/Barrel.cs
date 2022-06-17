using UnityEngine;

public class Barrel : MonoBehaviour
{
    public bool explosiv;
    public GameObject explosionFX;
    public GameObject[] drops;
    public AudioClip deathSound;

    public void HitBarrel()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 2f);
        if (!explosiv) return;
        var transform1 = transform;
        Instantiate(explosionFX, transform1.position, transform1.rotation);
        if (drops.Length == 0)
        {
            foreach (var drop in drops)
            {
                Instantiate(drop, transform1.position, transform1.rotation);
            }
        }
        Destroy(gameObject);
    }
}
