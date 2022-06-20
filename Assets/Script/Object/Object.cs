using UnityEngine;

public class Object : MonoBehaviour
{
    [Header("Settings")] //
    public bool explosive;
    public bool getsDestroyed;
    public GameObject[] containedPickups;
    public AudioClip hitSound;
    public float hitVolume = 2f;

    [Header("References")] //
    public GameObject objectGameObject;
    public Transform objectTransform;
    public GameObject explosionParticlePrefab;

    public void Hit()
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position, hitVolume);

        SpawnContainedPickups();

        if (explosive)
            Explode();

        if(getsDestroyed)
            Destroy(objectGameObject);
    }

    private void Explode()
    {
        Instantiate(explosionParticlePrefab, objectTransform.position, objectTransform.rotation);
    }

    private void SpawnContainedPickups()
    {
        foreach (var drop in containedPickups)
        {
            Instantiate(drop, objectTransform.position, objectTransform.rotation);
        }
    }
}
