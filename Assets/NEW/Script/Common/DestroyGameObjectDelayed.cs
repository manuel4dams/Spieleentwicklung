using UnityEngine;

public class DestroyGameObjectDelayed : MonoBehaviour
{
    public float lifetime = 1f;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
