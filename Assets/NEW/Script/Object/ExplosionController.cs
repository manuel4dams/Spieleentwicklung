using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ExplosionController
    {
        public ExplosionState state;
        public Transform origin;
        public Light lightSource;

        public GameObject currentHitObject { get; private set; }
        public event Action onHit;

        public void Explode()
        {
            var colliders = Physics.OverlapSphere(origin.position, state.explosionRadius);
            foreach (var collider in colliders)
            {
                currentHitObject = collider.gameObject;
                onHit?.Invoke();


                var rigidbody = collider.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    var originPosition = origin.position;
                    var offset = originPosition - collider.transform.position;
                    var forceFactor = 1f - offset.sqrMagnitude / (state.explosionRadius * state.explosionRadius);
                    rigidbody.AddExplosionForce(state.explosionForce * forceFactor, originPosition, state.explosionRadius, state.explosionUpwardsModifier, ForceMode.Impulse);
                }
            }
        }

        public void LightFlash()
        {
            lightSource.intensity = Mathf.Lerp(lightSource.intensity, 0f, 5 * Time.time);
        }
    }
}
