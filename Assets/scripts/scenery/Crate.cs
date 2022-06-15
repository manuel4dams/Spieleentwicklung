using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject[] drops;
    public AudioClip deathSound;

    public void DestroyCrate()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 2f);
        Destroy(gameObject.transform.parent.gameObject);
        if (drops.Length == 0) return;
        foreach (var drop in drops)
        {
            var transform1 = transform;
            Instantiate(drop, transform1.position, transform1.rotation);
        }
    }
}
