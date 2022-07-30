using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ExplosionState : MonoBehaviour
    {
        [Header("Settings")] //
        public float explosionForce;
        public float explosionUpwardsModifier;
        public float explosionRadius;
        public float damage;
    }
}
