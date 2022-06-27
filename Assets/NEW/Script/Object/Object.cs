using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Object : MonoBehaviour
    {
        [Header("Settings")] //
        public bool destroyable;
        public float healthAmount;
        public GameObject[] containedPickups;
        public AudioClip hitSound;
    }
}
