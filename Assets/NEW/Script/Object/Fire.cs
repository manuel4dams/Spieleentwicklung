using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Fire : MonoBehaviour
    {
        [Header("Settings")] //
        public float damage;
        public float fireTickRate;
    }
}
