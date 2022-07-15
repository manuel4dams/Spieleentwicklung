using UnityEngine;

public class DestroyGameObjectDelayed : MonoBehaviour
{
    public float lifetime = 1f;

    void Awake()
    {
        // TODO Endless lifetime should be triggered via -1 instead of 0
        if (lifetime == 0f)
            return;

        Destroy(gameObject, lifetime);
    }
}
