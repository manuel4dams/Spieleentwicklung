using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class HealthPickup : MonoBehaviour
    {
        public float healthAmount = 1f;
        public AudioClip healthPickupSound;
        public Transform audioSourceTransform;
    }
}
