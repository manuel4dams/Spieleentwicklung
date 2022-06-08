using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    public float lifetime = 1f;

    void Awake()
    {
        // destroy object at the end of the lifetime
        Destroy(gameObject, lifetime);
    }
}
