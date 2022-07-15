using GameGraph;
using JetBrains.Annotations;
using MyBox;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ZombieAnimation
    {
        // References
        public Animator animator;

        // Input
        public bool walking { private get; set; }
        public bool running { private get; set; }
        public bool attacking { private get; set; }

        public void Handle()
        {
            animator.SetBool("walk", walking);
            animator.SetBool("run", running);
            animator.SetBool("attack", attacking);
        }
    }
}
