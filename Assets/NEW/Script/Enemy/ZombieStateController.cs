using GameGraph;
using JetBrains.Annotations;
using MyBox;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ZombieStateController
    {
        // References
        public ZombieState zombieState;

        public void HandleZombieDeath()
        {
            //TODO handle zombie death via gamegraph
        }
    }
}
