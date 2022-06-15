using UnityEngine;

public class Barrel : MonoBehaviour
{
    public bool explosiv;
    public GameObject[] drops;
    public AudioClip deathSound;

    public void HitBarrel()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 2f);
        if (!explosiv) return;
        Destroy(gameObject.transform.parent.gameObject);
        if (drops.Length == 0) return;
        foreach (var drop in drops)
        {
            var transform1 = transform;
            Instantiate(drop, transform1.position, transform1.rotation);
        }
    }
}
