using UnityEngine;

public class DestroyGameObjectDelayed : MonoBehaviour
{
    public float lifetime = 1f;

    void Awake()
    {
        if (lifetime == 0f)
            return;

        Destroy(gameObject, lifetime);
    }
}
