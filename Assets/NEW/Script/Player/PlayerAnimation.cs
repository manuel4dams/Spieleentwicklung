using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayerAnimation
    {
        // References
        public Animator animator;

        // Input
        public bool sneaking { private get; set; }
        public bool grounded { private get; set; }
        public bool shooting { private get; set; }
        public float horizontalMovementSpeed { private get; set; }

        public void Handle()
        {
            // TODO Maybe change animator to accept booleans?
            animator.SetFloat("sneaking", sneaking ? 1f : 0f);
            animator.SetBool("grounded", grounded);
            animator.SetFloat("shooting", shooting ? 1f : 0f);
            animator.SetFloat("movementSpeed", horizontalMovementSpeed);
        }
    }
}
