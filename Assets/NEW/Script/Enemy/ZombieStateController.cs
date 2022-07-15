using GameGraph;
using JetBrains.Annotations;
using MyBox;
using Unity.VisualScripting;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ZombieStateController
    {
        // References
        public ZombieState zombieState;
        public SkinnedMeshRenderer skinnedMeshRenderer;


        // Health

        // Death

        public void SetupMaterial()
        {
            if (zombieState.material)
            {
                skinnedMeshRenderer.material = zombieState.material;
            }
            else if (zombieState.materials.NotNullOrEmpty())
            {
                skinnedMeshRenderer.material = zombieState.materials[Random.Range(0, zombieState.materials.Length)];
            }
        }
    }
}
