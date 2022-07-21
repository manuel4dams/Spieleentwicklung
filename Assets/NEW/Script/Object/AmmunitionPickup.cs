using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AmmunitionPickup : MonoBehaviour
    {
        public int ammunitionAmount;
        public AudioClip ammunitionPickupSound;
        public Transform audioSourceTransform;
    }
}
