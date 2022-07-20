using ScriptGG;
using UnityEngine;

public class Melee : Projectile
{
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
