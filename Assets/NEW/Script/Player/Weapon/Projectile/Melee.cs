using System;
using ScriptGG;
using Unity.VisualScripting;
using UnityEngine;

public class Melee : Projectile
{
    [Header("Fireball parameters")] //
    public float speed;

    [Header("References")] //
    public new Rigidbody rigidbody;

    // TODO Melee animation
    void Start()
    {
        rigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!LayerMaskHelper.LayerIsInMask(other.gameObject.layer, hittableLayerMask))
            return;

        var hittable = other.GetComponentInChildren<IHittable>();
        // Null propagation does not work with Unity
        // ReSharper disable once UseNullPropagation
        if (hittable == null)
            return;

        hittable.OnHit(damage);
    }
}
